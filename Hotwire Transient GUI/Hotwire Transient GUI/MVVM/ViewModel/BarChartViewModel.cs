using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System;
using Hotwire_Transient_GUI.Code;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
    public class BarChartViewModel
    {
        public ChartValues<ObservableValue> ChartPoints { get; set; } = new ChartValues<ObservableValue>();

        public ObservableCollection<Point> ChartTestData
        {
            get { return _ChartTestData; }
            set
            {
                _ChartTestData = value;
                plotPoints();
            }
        }
        public Func<double, string> yFormatter { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        

        private ObservableCollection<Point> _ChartTestData { get; set; }

        public BarChartViewModel()
        {
            yFormatter = value => value.ToString("G4");
        }

        public void plotPoints()
        {
            ChartPoints.Clear();
            //MinValue = ChartTestData[0].sample;
            //MinValue = ChartTestData[ChartTestData.Count].sample;
            ObservableValue[] tempChartPoints = new ObservableValue[ChartTestData.Count];
            for (int i = 0; i < ChartTestData.Count; i++)
            {
                tempChartPoints[i] = new ObservableValue(ChartTestData[i].wireTemp);
            }
            ChartPoints.AddRange(tempChartPoints);
        }

    }
    }


