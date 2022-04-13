using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code
{
    public class SingleHotWireTest
    {
        public ObservableCollection<Point> Data { get; set; }
        public ObservableCollection<Point> Error { get; set; }

        public double ThermalConductivity { get; set; }
        public double ThermalDiffusivity { get; set; }
        public double VolumetricHeatCapacity { get; set; }

        public double AverageError { get { return _AverageError; } }
        public double LargestError { get {return _LargestError; } }

        public int SlopeSelectionMin { get; set; }
        public int SlopeSelectionMax { get; set; }

        private double _AverageError;
        private double _LargestError;

        public double RegressionSlope { get; set; }
        public double RegressionIntercept { get; set; }

        public const double current = 0.015;
        public const double resistance = 1;
        public const double length = 0.05;
        public const double heatingPower = current * current * resistance / length;

        public SingleHotWireTest()
        {
            Data = new ObservableCollection<Point>();
            Error = new ObservableCollection<Point>();
        }


        public void LinearRegression(int Sections = 10)
        {
            int sectionSize = (int) Math.Floor((double) Data.Count / Sections);
            double[] linRegResults;
            double[] slopes = new double[Sections];
            double[] r2Values = new double[Sections];
            double[] intercepts = new double[Sections];
            for (int i = 0; i < Sections; i++)
            {
                linRegResults = lin_Regression(i * sectionSize, (i + 1) * sectionSize - 1); // may have to use &timeMatrix[i] if this doesnt work
                slopes[i] = linRegResults[0];
                r2Values[i] = linRegResults[1];
                intercepts[i] = linRegResults[2];
            }

            double maxR2 = 1;
            double tempR2 = r2Values[0];
            double linSlope = slopes[0];
            double intercept = intercepts[0];
            for (int i = 0; i < Sections; i++)
            {
                if(slopes[i] < 0)
                {
                    continue;
                }
                else if(r2Values[i] >= tempR2 || linSlope <= 0)
                {
                    tempR2 = r2Values[i];
                    linSlope = slopes[i];
                    intercept = intercepts[i];
                    SlopeSelectionMin = i * sectionSize;
                    SlopeSelectionMax = (i + 1) * sectionSize;
                }
            }

            if(linSlope <= 0 || maxR2 > 1)
            {
                //something wrong
            }

            //do
            //{
            //    tempR2 = maxR2;
            //    maxR2 = r2Values[0];
            //    linSlope = slopes[0];

            //    for (int i = 0; i < Sections; i++)
            //    {
            //        if (r2Values[i] > maxR2 && r2Values[i] < tempR2)
            //        {
            //            maxR2 = r2Values[i];
            //            linSlope = slopes[i];
            //            SlopeSelectionMin = i * sectionSize;
            //            SlopeSelectionMax = (i + 1) * sectionSize;
            //        }
            //    }
            //}
            //while (linSlope < 0);//repeat until slope in non-negative
            RegressionSlope = linSlope;
            RegressionIntercept = intercept;
            ThermalConductivity = heatingPower*HotWireTest.CalibrationCoefficient/(4*Math.PI*linSlope);
        }


        double[] lin_Regression(int StartPos, int EndPos)
        {
            double a, b, R2, ypred, ydiff, ydiff2, var, var2, yAvg;

            double sumx = 0;
            double sumy = 0;
            double sumx2 = 0;
            double sumy2 = 0;
            double sumxy = 0;
            double sumyDiff2 = 0;
            double totalVar = 0;
            int i;
            double tempX;

            for (i = StartPos; i < EndPos; i++)
            {
                tempX = Math.Log(Data[i].time);
                sumx = sumx + tempX;
                sumy = sumy + Data[i].wireTemp;
                sumx2 = sumx2 + tempX * tempX;
                sumy2 = sumy2 + Data[i].wireTemp * Data[i].wireTemp;
                sumxy = sumxy + tempX * Data[i].wireTemp;
            }
            int n = EndPos - StartPos;
            a = ((sumy * sumx2) - (sumx * sumxy)) / ((n * sumx2) - (sumx * sumx)); // y-intercept

            b = ((n * sumxy) - (sumx * sumy)) / ((n * sumx2) - (sumx * sumx)); // slope 

            for (i = StartPos; i < EndPos; i++)
            {
                ypred = a + b * Math.Log(Data[i].time);

                ydiff = Data[i].wireTemp - ypred;

                ydiff2 = ydiff * ydiff;

                yAvg = sumy / n;

                var = Data[i].wireTemp - yAvg;

                var2 = var * var;

                sumyDiff2 = sumyDiff2 + ydiff2;

                totalVar = totalVar + var2;
            }

            R2 = 1 - (sumyDiff2 / totalVar);
            double[] result = new double[3];
            result[0] = b;
            result[1] = R2;
            result[2] = a;
            return result;
        }
        




        //public void LinearRegression()
        //{
        //    double sumx, sumx2, sumy, sumy2, sumxy;
        //    sumx = 0;
        //    sumx2 = 0;
        //    sumy = 0;
        //    sumy2 = 0;
        //    sumxy = 0;
        //    double tempX;
        //    for (int i = 0; i < Data.Count; i++)
        //    {
        //        tempX = Math.Log(Data[i].time);
        //        sumx += tempX;
        //        sumx2 += tempX * tempX;
        //        sumy += Data[i].wireTemp;
        //        sumy2 += Data[i].wireTemp * Data[i].wireTemp;
        //        sumxy += tempX * Data[i].wireTemp;
        //    }

        //    RegressionSlope = ((sumy * sumx2) - (sumx * sumxy)) / ((Data.Count * sumx2) - (sumx * sumx));
        //    RegressionIntercept = ((Data.Count * sumxy) - (sumx * sumy)) / ((Data.Count * sumx2) - (sumx * sumx));

        //    ThermalConductivity = heatingPower/(4*Math.PI*RegressionSlope);
        //}


        public void CalculateError()
        {

            Error.Clear();
            double x, y;
            Point p;
            double averageSum, max;

            max = 0;
            averageSum = 0;
            for(int i = SlopeSelectionMin; i < SlopeSelectionMax; i++)
            {
                p = Data[i];
                x = Math.Log(p.time);
                y = RegressionSlope * x - p.wireTemp + RegressionIntercept;

                if(Math.Abs(y) > max)
                {
                    max = Math.Abs(y);
                }

                averageSum += Math.Abs(y);

                Error.Add(new Point(x, y, i));
            }

            _LargestError = max;
            _AverageError = averageSum/(SlopeSelectionMax-SlopeSelectionMin);
        }

    }
}
