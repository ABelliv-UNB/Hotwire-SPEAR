using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hotwire_Transient_GUI.Code;
using Microsoft.Win32;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
public class DataViewModel : ObservableObject
	{
        #region Properties
        #region Charts
        public ChartViewModel DataChart { get; set; }
		public BarChartViewModel ErrorChart { get; set; }
        #endregion
        #region Test
        public HotWireTest _Test;

        public HotWireTest Test
		{
			get{return _Test;}
            set {
				_Test = value;
				addTestSelector();
				OnPropertyChanged("Test");
				//OnPropertyChanged("SelectedTestIndex");
				//OnPropertyChanged("SelectedTest");
			}
		}

		public int _SelectedTestIndex { get; set; } = 1;
		public int SelectedTestIndex
		{
			get { return _SelectedTestIndex; }
			set
			{
				_SelectedTestIndex = value;
				DataChart.ChartTestData = Test.Tests[value - 1].Data;
				ErrorChart.ChartTestData = Test.Tests[value - 1].Error;
				ErrorChart.MinValue = Test.Tests[value - 1].SlopeSelectionMin;
				ErrorChart.MaxValue = Test.Tests[value - 1].SlopeSelectionMax;
				DataChart.SelectionMin = Math.Log10(Test.Tests[value - 1].Data[Test.Tests[value - 1].SlopeSelectionMin].time);
				DataChart.SelectionWidth = Math.Abs( Math.Log10(Test.Tests[value - 1].Data[Test.Tests[value - 1].SlopeSelectionMax].time) - Math.Log10(Test.Tests[value - 1].Data[Test.Tests[value - 1].SlopeSelectionMin].time)  );
				_SelectedTest = Test.Tests[value - 1];
				OnPropertyChanged("SelectedTestIndex");
				OnPropertyChanged("SelectedTest");
			}
		}

		private SingleHotWireTest _SelectedTest;
		public SingleHotWireTest SelectedTest
        {
            get { return _SelectedTest; }
        }

		public HotWireReadWrite TestReadWrite { get; set; }
        #endregion
        #region UI
        public ObservableCollection<UIElement> TestSelectButtons { get; set; } = new ObservableCollection<UIElement>();
		public StackPanel TestSelectPanel { get; set; }
		private RadioButton AverageButton;
        #endregion
        #endregion

        #region Constructor
        public DataViewModel()
		{
			DataChart = new ChartViewModel();
			ErrorChart = new BarChartViewModel();
			TestReadWrite = new HotWireReadWrite();
			TestSelectPanel = new StackPanel();
			initStackPanel();
			TestSelectButtons = new ObservableCollection<UIElement>();
			TestSelectButtons.Add(TestSelectPanel);
			Test = new HotWireTest();
			Test.SimulateTest();
			Test = Test;
		}

        private void initStackPanel()
        {
			TestSelectPanel.HorizontalAlignment = HorizontalAlignment.Center;
			TestSelectPanel.VerticalAlignment = VerticalAlignment.Center;
			TestSelectPanel.Orientation = Orientation.Horizontal;
        }
        #endregion
        private void addTestSelector()
		{
			TestSelectPanel.Children.Clear();

			for (int i = 0; i < Test.Tests.Count; i++)
			{
				RadioButton newButton = new RadioButton();
				newButton.Content = (i + 1).ToString();
				newButton.Style = (Style) Application.Current.FindResource("TestSelectorButton");
				newButton.Command = new RelayCommand(o =>
				{
					int index = Int32.Parse(newButton.Content.ToString()); //Questionable line of code which could cause errors
					SelectedTestIndex = index;
				});
				TestSelectPanel.Children.Add(newButton);

				//TestSelectButtons.Add(newButton);
				//TestSelectorButton
			}
			//One more
			AverageButton = new RadioButton();
			AverageButton.Content = "Average";
			AverageButton.Style = (Style)Application.Current.FindResource("TestSelectorButton");
			AverageButton.Command = new RelayCommand(o =>
			{
				ShowAverageOfTests();
			});
			TestSelectPanel.Children.Add(AverageButton);
		}

		public void ShowAverageOfTests()
        {
			_SelectedTest = Test.AverageOfTests;
			DataChart.ChartTestData = Test.AverageOfTests.Data;
			ErrorChart.ChartTestData = Test.AverageOfTests.Error;
			AverageButton.IsChecked = true;
			OnPropertyChanged("SelectedTestIndex");
			OnPropertyChanged("SelectedTest");
		}
	}
}
