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
    /// Interaction logic for OkModal.xaml
    /// </summary>
    public partial class OkModal : Window
    {
        public OkModal(string ModalText)
        {
            InitializeComponent();
            this.ModalText.Text = ModalText;
        }

        private void Button_Continue(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
