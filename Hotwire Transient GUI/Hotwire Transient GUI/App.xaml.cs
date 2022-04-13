using Hotwire_Transient_GUI.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Hotwire_Transient_GUI.Code;
using System.IO.Ports;
using Hotwire_Transient_GUI.FirstTimeSetup;

namespace Hotwire_Transient_GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {            
            base.OnStartup(e);
            this.Resources["MainColor"] = new SolidColorBrush(Hotwire_Transient_GUI.Properties.Settings.Default.MainColor.ToSWMColor());
            this.Resources["MainColorAsColor"] = Hotwire_Transient_GUI.Properties.Settings.Default.MainColor.ToSWMColor();
            this.Resources["AccentColor"] = new SolidColorBrush(Hotwire_Transient_GUI.Properties.Settings.Default.AccentColor.ToSWMColor());
            this.Resources["CardColor"] = new SolidColorBrush(Hotwire_Transient_GUI.Properties.Settings.Default.CardColor.ToSWMColor());
            this.Resources["TextColor"] = new SolidColorBrush(Hotwire_Transient_GUI.Properties.Settings.Default.TextColor.ToSWMColor());
            this.Resources["GlowColor"] = Hotwire_Transient_GUI.Properties.Settings.Default.GlowColor.ToSWMColor();
            this.Resources["ScrollBarColor"] = new SolidColorBrush(Hotwire_Transient_GUI.Properties.Settings.Default.ScrollBarColor.ToSWMColor());
        }
    }
}
