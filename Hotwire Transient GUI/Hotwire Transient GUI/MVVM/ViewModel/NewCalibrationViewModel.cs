using Hotwire_Transient_GUI.Code;
using Hotwire_Transient_GUI.Code.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
    public class NewCalibrationViewModel : ObservableObject
    {
        #region Properties
        #region Material
        public static List<CalibrationReference> CalibrationReferences { get; set; }
        private CalibrationReference _SelectedMaterial;
        public CalibrationReference SelectedMaterial {
            get
            {
                return _SelectedMaterial;
            }
            set 
            {
                _SelectedMaterial = value;
                OnPropertyChanged("ReferenceTCString");
                OnPropertyChanged("ReferenceTempString");
            }
        }
        #endregion
        #region Calibration Properties
        public string ReferenceTCString
        {
            get 
            {
                if (SelectedMaterial != null)
                {
                    RoundDoubleConverter r = new RoundDoubleConverter();
                    return (string) r.Convert(SelectedMaterial.ThermalConductivity, typeof(double), null, null) ;
                }
                else
                {
                    return string.Empty;
                }
            } 
        }
        public string ReferenceTempString
        {
            get
            {
                if (SelectedMaterial != null)
                {
                    return SelectedMaterial.RoomTemperature.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Tolerance { get;set; }

        #endregion
        #region Tolerance
        private bool canStartCalibration;
        private double _Tolerance;
        private string _ToleranceString;

        public string ToleranceString
        {
            get { return _ToleranceString; }
            set
            {
                _ToleranceString = value;
                if (double.TryParse(value, out _Tolerance) && _Tolerance > 0)
                {
                    canStartCalibration = true;
                }
                else
                {
                    canStartCalibration = false;
                }
            }
        }
        #endregion
        public event EventHandler<CalibrationEventArgs> NewCalibration;
        public RelayCommand StartCalibrationCommand { get; set; }
        #endregion
        public NewCalibrationViewModel()
        {
            CalibrationReferences = new List<CalibrationReference>();
            CalibrationReferences.Add(new CalibrationReference("Distilled Water", 0.598));
            StartCalibrationCommand = new RelayCommand(o =>
            {
                StartCalibration();
            });
        }

        private void StartCalibration()
        {
            if (canStartCalibration)
            {
                OnStartCalibration(this, new CalibrationEventArgs(SelectedMaterial, _Tolerance));
            }
        }


        protected virtual void OnStartCalibration(object sender, CalibrationEventArgs e)
        {
            EventHandler<CalibrationEventArgs> handler = NewCalibration;
            handler?.Invoke(this, e);
        }

        
    }
}
