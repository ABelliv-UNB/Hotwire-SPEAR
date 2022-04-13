using Hotwire_Transient_GUI.Code;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
    public class CalibrationViewModel : ObservableObject
    {

		public double Tolerance { get; set; }
		public double ErrorPercentage {
            get
            {
				return Math.Abs(ResultConductivity - ReferenceConductivity) / ReferenceConductivity;
            }
		}

		public HotWireSerialCom PortManager { get; set; }
		public double CalibrationCoefficent
        {
			get
			{
				if (ResultConductivity != 0)
				{
					return ReferenceConductivity / ResultConductivity;
				}
				else return double.NaN;
			}
        }

		public double ReferenceConductivity
        {
            get
            {
				return CalibrationMaterial.ThermalConductivity;
            }
        }
		public double ResultConductivity
		{
			get
			{
				return CalibrationData.ThermalConductivity;
			}
		}

		public CalibrationReference CalibrationMaterial { get; set; }

		public ChartViewModel DataChart { get; set; }
		public ChartViewModel ErrorChart { get; set; }

		private SingleHotWireTest _CalibrationData;
		public SingleHotWireTest CalibrationData 
		{
            get
            {
				return _CalibrationData;
            }
            set
            {
				_CalibrationData = value;
				DataChart.ChartTestData = CalibrationData.Data;
				ErrorChart.ChartTestData = CalibrationData.Error;
			}
		}


		public CalibrationViewModel(HotWireSerialCom PortManager)
		{
			DataChart = new ChartViewModel();
			ErrorChart = new ChartViewModel();
			CalibrationData = new SingleHotWireTest();
			this.PortManager = PortManager;
            PortManager.CalibrationDataReceived += CalibrationDataReceived;
		}

        private void CalibrationDataReceived(object sender, Code.Events.CalibrationDataEventArgs e)
        {
			this.CalibrationData = e.CalibraitonData;
			this.Tolerance = e.Tolerance;
        }
    }
}



