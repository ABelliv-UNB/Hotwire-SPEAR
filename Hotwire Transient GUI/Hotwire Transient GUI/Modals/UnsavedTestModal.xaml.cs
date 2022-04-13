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
using System.Windows.Shapes;

namespace Hotwire_Transient_GUI.Modals
{
    /// <summary>
    /// Interaction logic for UnsavedTestModal.xaml
    /// </summary>
    public partial class UnsavedTestModal : Window
    {
        public string FullFileName { get; set; }
        private bool _Save = false;
        public bool Save { get { return _Save; } }
        public UnsavedTestModal()
        {
            InitializeComponent();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            SaveLocationModal saveLocationModal = new SaveLocationModal();
            saveLocationModal.Owner = this;
            saveLocationModal.ShowDialog();
            if (saveLocationModal.SaveFile)
            {
                FullFileName = saveLocationModal.FullFileName;
                _Save = true;
                this.Close();
            }
        }

        private void Button_Discard(object sender, RoutedEventArgs e)
        {
            _Save = false;
            this.Close();
        }
    }
}
