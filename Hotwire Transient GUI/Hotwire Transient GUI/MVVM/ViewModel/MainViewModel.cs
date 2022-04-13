using Hotwire_Transient_GUI.Code;
using Hotwire_Transient_GUI.Code.Events;
using Hotwire_Transient_GUI.Modals;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        #region Properties

        #region Views
        //Data
        public DataViewModel DataView { get; set; }
        public RelayCommand ShowDataView { get; set; }
        //Histoy
        public HistoryViewModel HistoryView { get; set; }
        public RelayCommand ShowHistoryView { get; set; }
        //New Test
        public NewTestViewModel NewTestView { get; set; }
        public RelayCommand ShowNewTestView { get; set; }
        //Calibration
        public CalibrationViewModel CalibrationView { get; set; }
        //public RelayCommand ShowCalibrationView { get; set; }
        public NewCalibrationViewModel NewCalibrationView { get; set; }
        public RelayCommand ShowNewCalibrationView { get; set; }
        //Save
        public RelayCommand SaveCurrentTest { get; set; }

        public RelayCommand ShowSettingsView { get; set; }
        public SettingsViewModel SettingsView { get; set; }
        #endregion
        public HotWireSerialCom SerialCom { get; set; }

        public event EventHandler<ShowDialogEventArgs> ShowWindow;
        public event EventHandler<SelectedButtonChangedEventArgs> SelectedButtonChanged;

        private bool _TestSaved;
        public bool TestSaved { get { return _TestSaved; } }

        private object _currentView;

		public object CurrentView
        {
			get{ return _currentView; }
            set {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            SerialCom = new HotWireSerialCom();

            try
            {
                SerialPort port = new SerialPort();
                port.PortName = Hotwire_Transient_GUI.Properties.Settings.Default.CommChannel;
                port.BaudRate = 115200;
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.Parity = Parity.None;
                SerialCom.Port = port;
            }
            catch (Exception)
            {

            }


            DataView = new DataViewModel();
            HistoryView = new HistoryViewModel();
            NewTestView = new NewTestViewModel(SerialCom);
            CalibrationView = new CalibrationViewModel(SerialCom);
            NewCalibrationView = new NewCalibrationViewModel();
            SettingsView = new SettingsViewModel(); 

            ShowDataView = new RelayCommand(o =>
            {
                CurrentView = DataView;
            });

            ShowHistoryView = new RelayCommand(o =>
            {
                CurrentView = HistoryView;
                HistoryView.updateFiles();
            });

            ShowNewTestView = new RelayCommand(o =>
            {
                CurrentView = NewTestView;
            });

            ShowNewCalibrationView = new RelayCommand(o =>
            {
                CurrentView = NewCalibrationView;
            });

            SaveCurrentTest = new RelayCommand(o =>
            {
                SaveTest(DataView.Test);
            });

            ShowSettingsView = new RelayCommand(o =>
            {
                CurrentView = SettingsView;
            });

            NewTestView.NewTest += ShowTestInDataView;
            NewTestView.ShowWindow += OnShowWindow;                          
            HistoryView.NewTest += ShowTestInDataView;
            NewCalibrationView.NewCalibration += ShowCalibrationView;
            HistoryView.IOError += OnShowWindow;
            

            SettingsView.NewPort += PortChanged;
            _TestSaved = true;
        }
        #endregion


        public void PortChanged(object sender, PortChangedEventArgs e)
        {
            SerialCom.Port = e.Port;
        }

        public void ShowTestInDataView(object sender, NewTestEventArgs e)
        {
            if (!TestSaved)
            {
                UnsavedTestModal unsavedTestModal = new UnsavedTestModal();
                OnShowWindow(this, new ShowDialogEventArgs(unsavedTestModal));
                if (unsavedTestModal.Save)
                {
                    SaveBeforeNewTest(DataView.Test);
                }
                else
                {
                    ShowTestInDataView(e.HotWireTest);
                    if (sender is HistoryViewModel)
                    {
                        _TestSaved = true;
                    }
                }
            }
            else
            {
                ShowTestInDataView(e.HotWireTest);
                if (sender is HistoryViewModel)
                {
                    _TestSaved = true;
                }
            }
        }

        public void ShowTestInDataView(HotWireTest test)
        {
            CurrentView = DataView;
            DataView.Test = test;
            if(DataView.SelectedTestIndex == 1)
            {
                DataView.SelectedTestIndex = 2;
            }
            DataView.SelectedTestIndex = 1;
            _TestSaved = false;
            //DataView.ShowAverageOfTests();
            OnSelectedButtonChanged(this, new SelectedButtonChangedEventArgs("Data", true));
        }

        public void SaveTest(HotWireTest Test)
        {
            SaveLocationModal saveLocationModal = new SaveLocationModal();
            OnShowWindow(this, new ShowDialogEventArgs(saveLocationModal));
            if (!saveLocationModal.Cancelled && saveLocationModal.FullFileName != String.Empty)
            {
                HotWireReadWrite.WriteTest(Test, saveLocationModal.FullFileName);
                _TestSaved = true;
            }
            OnSelectedButtonChanged(this, new SelectedButtonChangedEventArgs("Save", false));
        }

        public void SaveBeforeNewTest(HotWireTest Test){

        if(Test == null)
        {
            OnShowWindow(this, new ShowDialogEventArgs(new OkModal("No test to save")));
            return;
        }

            UnsavedTestModal unsavedTestModal = new UnsavedTestModal();
            bool exitSaveLoop = false;
            while (!exitSaveLoop)
            {
                OnShowWindow(this, new ShowDialogEventArgs(unsavedTestModal));
                if (unsavedTestModal.Save)
                {
                    try
                    {
                        exitSaveLoop = true;
                    }
                    catch (IOException)
                    {
                        OnShowWindow(this, new ShowDialogEventArgs(new OkModal("Something went wrong and the test could not be saved")));
                        exitSaveLoop = false;
                    }
                }
                else
                {
                    exitSaveLoop = true; //It's 5AM and I can't think of a better way to do this logic
                }
            }
        }

        public void ShowCalibrationView(object sender, CalibrationEventArgs e)
        {
            CurrentView = CalibrationView;
            CalibrationView.Tolerance = e.Tolerance;
            CalibrationView.CalibrationMaterial = e.CalibrationMaterial;
            SerialCom.StartCalibration(e.Tolerance);
        }

        protected virtual void OnShowWindow(object sender, ShowDialogEventArgs e)
        {
            EventHandler<ShowDialogEventArgs> handler = ShowWindow;
            handler?.Invoke(this, e);
        }

        protected virtual void OnSelectedButtonChanged(object sender, SelectedButtonChangedEventArgs e)
        {
            EventHandler<SelectedButtonChangedEventArgs> handler = SelectedButtonChanged;
            handler?.Invoke(this, e);
        }
    }
}
