using Hotwire_Transient_GUI.Code;
using Microsoft.Win32;
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

namespace Hotwire_Transient_GUI.Modals
{
    /// <summary>
    /// Interaction logic for SaveLocationModal.xaml
    /// </summary>
    public partial class SaveLocationModal : Window
    {

        public string FullFileName { get; set; }
        public string FileName { get; set; }
        public bool SaveFile { get; set; }
        public bool Cancelled { get; set; } = false;

        public SaveLocationModal()
        {
            InitializeComponent();
        }

        public void Cancel()
        {
            SaveFile = false;
            Cancelled = true;
            this.Close();
        }

        public void BrowseForSaveLocation()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.csv)|*.csv";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.FileName = FileNameTextBox.Text;
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() ?? false)
            {
                FullFileName = saveFileDialog.FileName;
                this.Close();
            }
        }

        public void SaveToDefaultFolder()
        {
            FullFileName = HotWireReadWrite.SelectedDirectory + "\\" + FileNameTextBox.Text + ".csv";
            if (!Directory.Exists(HotWireReadWrite.SelectedDirectory))
            {
                OkModal okModal = new OkModal("Default directory does not exist (can be changed in settings)");
                okModal.Owner = this;
                okModal.ShowDialog();
            }
            else if (FileNameTextBox.Text == string.Empty) 
            {
                OkModal okModal = new OkModal("Please give the file a name");
                okModal.Owner = this;
                okModal.ShowDialog();
            }
            else if (File.Exists(FullFileName))
            {
                OkModal okModal = new OkModal("File name already exists in that directory. Please choose a different name");
                okModal.Owner = this;
                okModal.ShowDialog();
            }
            else
            {
                SaveFile = true;
                Cancelled = false;
                this.Close();
            }
        }

        private void Button_Browse(object sender, RoutedEventArgs e)
        {
            BrowseForSaveLocation();
        }

        private void Button_SaveDefault(object sender, RoutedEventArgs e)
        {
            SaveToDefaultFolder();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Cancel();
        }
    }
}
