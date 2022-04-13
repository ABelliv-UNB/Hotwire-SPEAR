using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotwire_Transient_GUI.Code;
using Hotwire_Transient_GUI.Code.Events;
using Hotwire_Transient_GUI.FirstTimeSetup;
using Hotwire_Transient_GUI.MVVM.ViewModel;
using LiveCharts;

namespace Hotwire_Transient_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            if (Hotwire_Transient_GUI.Properties.Settings.Default.FirstSetup)
            {
                Setup s = new Setup();
                s.ShowDialog();
                Hotwire_Transient_GUI.Properties.Settings.Default.FirstSetup = false;
            }
            mainViewModel = (MainViewModel) this.DataContext;
            mainViewModel.ShowWindow += ShowWindow;
            mainViewModel.SelectedButtonChanged += SelectButton;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void ShowWindow(object sender, ShowDialogEventArgs e)
        {
            Fog.Visibility = Visibility.Visible;
            e.Dialog.Owner = this;
            if (e.WaitUntilClose)
            {
                e.Dialog.ShowDialog();
            }
            else
            {
                e.Dialog.Show();
            }
            Fog.Visibility = Visibility.Hidden;

        }

        private void SelectButton(object sender, SelectedButtonChangedEventArgs e)
        {
            if(e.Button == "Save")
            {
                SaveButton.IsChecked = e.IsSelected;
            }
            if (e.Button == "Data")
            {
                DataButton.IsChecked = e.IsSelected;
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
        public void UpdateSettings()
        {
            Application app = Application.Current;
            Hotwire_Transient_GUI.Properties.Settings.Default.AccentColor = ((SolidColorBrush)app.Resources["AccentColor"]).Color.ToSDColor();
            Hotwire_Transient_GUI.Properties.Settings.Default.MainColor = ((SolidColorBrush)app.Resources["MainColor"]).Color.ToSDColor();
            Hotwire_Transient_GUI.Properties.Settings.Default.CardColor = ((SolidColorBrush)app.Resources["CardColor"]).Color.ToSDColor();
            Hotwire_Transient_GUI.Properties.Settings.Default.TextColor = ((SolidColorBrush)app.Resources["TextColor"]).Color.ToSDColor();
            Hotwire_Transient_GUI.Properties.Settings.Default.GlowColor = ((Color)app.Resources["GlowColor"]).ToSDColor();
            Hotwire_Transient_GUI.Properties.Settings.Default.ScrollBarColor = ((SolidColorBrush)app.Resources["ScrollBarColor"]).Color.ToSDColor();
            Hotwire_Transient_GUI.Properties.Settings.Default.DefaultDirectory = HotWireReadWrite.SelectedDirectory;
            Hotwire_Transient_GUI.Properties.Settings.Default.Save();
        }
    }
}
