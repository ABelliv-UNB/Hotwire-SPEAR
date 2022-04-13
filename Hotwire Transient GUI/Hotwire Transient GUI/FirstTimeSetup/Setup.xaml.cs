using Hotwire_Transient_GUI.Code;
using Hotwire_Transient_GUI.Modals;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Hotwire_Transient_GUI.FirstTimeSetup
{
    /// <summary>
    /// Interaction logic for Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        public Setup()
        {
            InitializeComponent();
        }


        private void Button_Browse(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderTextBox.Text = dialog.FileName;
            }
        }

        private void Button_Done(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(FolderTextBox.Text))
            {
                HotWireReadWrite.SelectedDirectory = FolderTextBox.Text;
                this.Close();
            }
            else
            {
                OkModal okModal = new OkModal("Directory does not exist");
                Fog.Visibility = Visibility.Visible;
                okModal.Owner = this;
                okModal.ShowDialog();
                Fog.Visibility = Visibility.Hidden;

            }
        }
    }
}
