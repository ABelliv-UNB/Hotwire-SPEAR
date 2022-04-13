using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using System;
using Hotwire_Transient_GUI.Code;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
    public class ChartViewModel : ObservableObject
    {
        //The points which are shown on the chart
        public ChartValues<ObservablePoint> ChartPoints { get; set; } = new ChartValues<ObservablePoint>();
        public Func<double, string> xFormatter { get; set; }
        public Func<double, string> yFormatter { get; set; }

        private double _SelectionMin;
        private double _SelectionWidth;
        public double SelectionMin
        {
            get
            {
                return _SelectionMin;
            }
            set
            {
                _SelectionMin = value;
                OnPropertyChanged("SelectionMin");
            }
        }
        public double SelectionWidth
        {
            get
            {
                return _SelectionWidth;
            }
            set
            {
                _SelectionWidth = value;
                OnPropertyChanged("SelectionWidth");
            }
        }

        public double Base { get; set; } = 10;


        //The points which come from a thermal transient test
        public ObservableCollection<Point> ChartTestData
        {
            get { return _ChartTestData; }
            set
            {
                _ChartTestData = value;
                plotPoints();
            }
        }
        private ObservableCollection<Point> _ChartTestData { get; set; }

        public ChartViewModel()
        {
            RoundDoubleConverter r = new RoundDoubleConverter();
            xFormatter = value => (string) r.Convert(Math.Pow(Base, value), typeof(string), null, null);
            yFormatter = value => value.ToString("G4");
        }



        public void plotPoints()
        {
            ChartPoints.Clear();
            ObservablePoint[] tempChartPoints = new ObservablePoint[ChartTestData.Count];
            for (int i = 0; i < ChartTestData.Count; i++)
            {
                tempChartPoints[i] = new ObservablePoint(Math.Log(ChartTestData[i].time, Base), ChartTestData[i].wireTemp);
            }
            ChartPoints.AddRange(tempChartPoints);
        }
    }
}
