using Hotwire_Transient_GUI.Code;
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

namespace Hotwire_Transient_GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for NewCalibrationView.xaml
    /// </summary>
    public partial class NewCalibrationView : UserControl
    {
        private bool canStartCalibration = false;
        private Brush normalBorderBrush;
        private Brush normalButtonBrush;
        public NewCalibrationView()
        {
            InitializeComponent();
            normalBorderBrush = ToleranceTextBox.BorderBrush;
            normalButtonBrush = CalibrateButton.Background;
        }

        private void TextBox_ToleranceChanged(object sender, TextChangedEventArgs e)
        {
            double tolerance;

            if (double.TryParse(ToleranceTextBox.Text, out tolerance) && tolerance > 0)
            {
                ToleranceTextBox.BorderBrush = normalBorderBrush;
                CalibrateButton.Background = normalButtonBrush;
                canStartCalibration = true;                
            }
            else
            {
                ToleranceTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                canStartCalibration = false;
                CalibrateButton.Background = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
