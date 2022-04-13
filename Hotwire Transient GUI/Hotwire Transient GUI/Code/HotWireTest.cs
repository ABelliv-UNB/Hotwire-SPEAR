using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotwire_Transient_GUI.Code;

namespace Hotwire_Transient_GUI.Code
{
    public class HotWireTest
    {
        public List<SingleHotWireTest> Tests { get; set; }
        public SingleHotWireTest AverageOfTests { get; set;}
        public String Material { get; set; }
        public static int SignificantDigits = 5;
        public static double CalibrationCoefficient = 1;
        public DateTime Date { get; set; }

        public double ThermalConductivity { get; set; }
        public double ThermalDiffusivity { get; set; }
        public double VolumetricHeatCapacity { get; set; }

        public string ThermalConductivityString
        {
            get { return ThermalConductivity.ToString() + "W/(mK)"; }
        }
        public string ThermalDiffusivityString
        {
            get { return ThermalDiffusivity.ToString() + "m²/s"; }
        }
        public string VolumetricHeatCapacityString
        {
            get { return VolumetricHeatCapacity.ToString() + "J/(m³K)"; }
        }
        public HotWireTest(int testCount = 10)
        {
            //Create list for the tests
            Tests = new List<SingleHotWireTest>();
            //Create 10 tests
            for(int i = 0; i < testCount; i++)
            {
                Tests.Add(new SingleHotWireTest());
            }
            ThermalConductivity = -1;
            ThermalDiffusivity = -1;
            VolumetricHeatCapacity = -1;
        }
        
        public void SimulateTest()
        {
            //double cooldown = 30;
            double heating = 0.8;
            double testTime = heating;
            double sampleRate = 1500f;
            double tempDeviation = 4;
            double startValue = 21;
            double time;
            double value;
            double noise = 0.1;
            double heatedValue = 0;
            Random r = new Random();
            //Replace this code with coded to retrieve points from the Nucleo board
            foreach(SingleHotWireTest t in Tests)
            {
                for (int i = 1; i < sampleRate * testTime; i++) {
                    time = i / sampleRate;

                    value = (startValue + tempDeviation - tempDeviation * Math.Exp(- time / heating * 5)) + (r.NextDouble() - 0.5) * noise;
                    heatedValue = value;
                    t.Data.Add(new Point(time, value, i));



                }
                t.LinearRegression(10);
                t.CalculateError();
            }
            CreateAverageofTests();
            Material = "Water";
                Date = DateTime.Now;
        }

        public void CreateAverageofTests()
        {
            AverageOfTests = new SingleHotWireTest();
            double TCSum = 0;
            double TDSum = 0;
            double VHCSum = 0;
            double InterceptSum = 0;
            double SlopeSum = 0;

            int longestTest = 0;
            for (int testIndex = 0; testIndex < Tests.Count; testIndex++)
            {
                if(Tests[testIndex].Data.Count > longestTest)
                {
                    longestTest = Tests[testIndex].Data.Count;
                }
            }
            for (int testIndex = 0; testIndex < Tests.Count; testIndex++)
            {
                for (int pointIndex = 0; pointIndex < longestTest; pointIndex++)
                {
                    if (Tests[testIndex].Data.Count < pointIndex)
                    {
                        AverageOfTests.Data.Add(new Point(Tests[testIndex].Data[pointIndex].time, Tests[testIndex].Data[pointIndex].wireTemp, Tests[testIndex].Data[pointIndex].thermocouple, pointIndex));
                    }
                }
            }

            //big sort
            AverageOfTests.Data = new ObservableCollection<Point>(AverageOfTests.Data.OrderBy(i => i));

            for (int testIndex = 0; testIndex < Tests.Count; testIndex++)
            {
                TCSum += Tests[testIndex].ThermalConductivity;
                TDSum += Tests[testIndex].ThermalDiffusivity;
                VHCSum += Tests[testIndex].VolumetricHeatCapacity;
                InterceptSum += Tests[testIndex].RegressionIntercept;
                SlopeSum += Tests[testIndex].RegressionSlope;
            }

            AverageOfTests.ThermalConductivity = TCSum/Tests.Count;
            AverageOfTests.ThermalDiffusivity = TDSum / Tests.Count;
            AverageOfTests.VolumetricHeatCapacity = VHCSum / Tests.Count;
            AverageOfTests.RegressionIntercept = InterceptSum/Tests.Count;
            AverageOfTests.RegressionSlope = SlopeSum / Tests.Count;

            AverageOfTests.LinearRegression();
            AverageOfTests.CalculateError();
        }
    }
}
