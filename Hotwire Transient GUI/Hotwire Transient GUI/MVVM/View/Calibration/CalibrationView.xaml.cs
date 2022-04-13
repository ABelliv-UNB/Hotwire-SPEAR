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
	/// Interaction logic for CalibrationView.xaml
	/// </summary>
	public partial class CalibrationView : UserControl
	{
		private double ChartHWRatio = 3;
		public CalibrationView()
		{
			InitializeComponent();
		}

        private void MainChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
			ChartView chart = (ChartView)sender;
			chart.Height = ActualWidth / ChartHWRatio;
        }
    }
}
