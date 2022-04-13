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
	/// Interaction logic for DataView.xaml
	/// </summary>
	public partial class DataView : UserControl
	{
		private int PointsDisplayWidth = 275;
		private int PointsDisplayHideWidth = 900;
		private double ChartHWRatio = 2.5;
		private int MinChartHeight = 200;

		public DataView()
		{
			InitializeComponent();
		}

        private void MainChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
			double temp = MainChart.ActualWidth / ChartHWRatio;
			if(temp > MinChartHeight)
            {
				MainChart.Height = temp;
			}
            else
            {
				MainChart.Height = MinChartHeight;
			}
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
			if (this.ActualWidth < PointsDisplayHideWidth)
			{
				RightGridPanel.Visibility = Visibility.Collapsed;
				PointsDisplayCol.Width = GridLength.Auto;
			}
            else
            {
				RightGridPanel.Visibility = Visibility.Visible;
				PointsDisplayCol.Width = new GridLength(PointsDisplayWidth) ;
			}
        }
    }

	

}
