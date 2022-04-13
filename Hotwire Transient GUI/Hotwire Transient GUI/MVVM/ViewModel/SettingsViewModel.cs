using Hotwire_Transient_GUI.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.IO.Ports;
using Hotwire_Transient_GUI.Code.Events;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
    public class SettingsViewModel
    {

        public List<string> CommChannels 
        {
            get { return SerialPort.GetPortNames().ToList(); }
        }
        public event EventHandler<PortChangedEventArgs> NewPort;
        private string _SelectedCommChannel;

        public string SelectedCommChannel
        {
            get { return _SelectedCommChannel; }
            set 
            {
                string[] Ports = SerialPort.GetPortNames();
                if (!Ports.Contains(value)) { return; }

                _SelectedCommChannel = value;
                SerialPort port = new SerialPort();
                port.PortName = value;
                Hotwire_Transient_GUI.Properties.Settings.Default.CommChannel = value;
                try
                {
                    OnPortChange(new PortChangedEventArgs(port));
                }
                catch (Exception)
                {
                    //Handle this
                }
            }

        }

        public bool DarkMode
        {
            get
            {
                return Hotwire_Transient_GUI.Properties.Settings.Default.DarkMode;
            }
            set
            {
                Hotwire_Transient_GUI.Properties.Settings.Default.DarkMode = value;
                Application app = App.Current;
                //I love hard coded values
                if (value)
                {
                    app.Resources["AccentColor"] = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
                    app.Resources["CardColor"] = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30));
                    app.Resources["TextColor"] = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
                    app.Resources["GlowColor"] = Color.FromArgb(255, 30, 30, 30);
                    app.Resources["ScrollBarColor"] = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
                }
                else
                {
                    app.Resources["AccentColor"] = new SolidColorBrush(Color.FromArgb(255, 248, 248, 248));
                    app.Resources["CardColor"] = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    app.Resources["TextColor"] = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    app.Resources["GlowColor"] = Color.FromArgb(255, 238, 238, 238);
                    app.Resources["ScrollBarColor"] = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                }
            }
        }

        public SettingsViewModel()
        {
            if (Hotwire_Transient_GUI.Properties.Settings.Default.CommChannel != string.Empty)
            {
                SelectedCommChannel = Hotwire_Transient_GUI.Properties.Settings.Default.CommChannel;
            }
        }

        public string DefaultDirectory
        {
            get
            {
                return HotWireReadWrite.SelectedDirectory;
            }
            set
            {
                HotWireReadWrite.SelectedDirectory = value;
            }
        }


        protected virtual void OnPortChange(PortChangedEventArgs e)
        {
            EventHandler<PortChangedEventArgs> handler = NewPort;
            handler?.Invoke(this, e);
        }
    }
}
