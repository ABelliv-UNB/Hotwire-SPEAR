using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotwire_Transient_GUI.Code;
using System.IO;

namespace Hotwire_Transient_GUI.Code
{
    public class HotWireReadWrite
    {

        public static string SelectedDirectory { get; set; }

        #region Writing
        private static int testWidth = 4;
        private static int TestDateCol = 0;
        private static int TestTimeCol = 1;
        private static int TestCountCol = 2;
        private static int TestMaterialCol = 3;

        public static void WriteTest(HotWireTest hotWireTest, String FileName)
        {
            if(hotWireTest.Tests.Count <= 0) { return; }

            // Create a file to write to.
            using StreamWriter sw = File.CreateText(FileName);
            int longestTest = MaxTestPointCount(hotWireTest);
            sw.WriteLine(CreateTestParametersLine(hotWireTest));
            sw.WriteLine(CreateHeaders(hotWireTest));

            for(int i = 0; i < longestTest; i++)
            {
                sw.WriteLine(CreateLine(hotWireTest, i));
            }

        }

        private static int MaxTestPointCount(HotWireTest hotWireTest)
        {
            int maxLength = -1;
            foreach(SingleHotWireTest s in hotWireTest.Tests)
            {
                if (s.Data.Count > maxLength)
                {
                    maxLength = s.Data.Count;
                }
            }
            return maxLength;
        }

        private static string CreateLine(HotWireTest hotWireTest, int lineIndex)
        {
            string line = "";
            string lineAddition = "";
            for(int i = 0; i < hotWireTest.Tests.Count; i++)
            {
                try
                {
                    lineAddition = hotWireTest.Tests[i].Data[lineIndex].sample.ToString() + ",";
                    lineAddition += hotWireTest.Tests[i].Data[lineIndex].time.ToString() + ",";
                    lineAddition += hotWireTest.Tests[i].Data[lineIndex].wireTemp.ToString() + ",";
                    lineAddition += ",";
                }
                catch(ArgumentOutOfRangeException)
                {
                    lineAddition = ",,,,"; //Leave that part blank
                }
                line += lineAddition;
            }
            return line;
        }

        private static string CreateHeaders(HotWireTest hotWireTest)
        {
            string line = "";
            for(int i = 0; i < hotWireTest.Tests.Count; i++)
            {
                line += "Sample Number,";
                line += "Time,";
                line += "Value,";
                line += ",";
            }
            return line;

        }

        private static string CreateTestParametersLine(HotWireTest hotWireTest)
        {
            //Mandatory Fields
            string line = hotWireTest.Date.ToString("yyyy-MM-dd") + "," + hotWireTest.Date.ToString("HH:mm") + "," + hotWireTest.Tests.Count;
            if (hotWireTest.Material != null)
            {
                line += "," + hotWireTest.Material;
            }
            line += ",";



            return line;
        }
        #endregion

        #region Reading

        public static HotWireTest ReadTest(string FileName)
        {

            string[] Lines = File.ReadAllLines(FileName);

            int dataCount = Lines.Length - 2;

            string[] firstLine = Lines[0].Split(',');
            int testCount = Int32.Parse(firstLine[HotWireReadWrite.TestCountCol]);

            HotWireTest hotWireTest = new HotWireTest(testCount);
            DateTime dateTime;
            if(DateTime.TryParse(firstLine[HotWireReadWrite.TestDateCol]+ " " + firstLine[HotWireReadWrite.TestTimeCol], out dateTime))
            {
                hotWireTest.Date = dateTime;
            }
       
            hotWireTest.Material = firstLine[HotWireReadWrite.TestMaterialCol];

            for (int i = 2; i < dataCount + 2; i++)
            {
                readDataLine(Lines[i], hotWireTest);
            }


            foreach (SingleHotWireTest s in hotWireTest.Tests)
            {
                s.LinearRegression();
                s.CalculateError();
            }
            hotWireTest.CreateAverageofTests();

            return hotWireTest;
        }

        private static void readDataLine(string Line, HotWireTest hotWireTest)
        {
            string[] values = Line.Split(',');
            //quick sanity check
            if(values.Length/HotWireReadWrite.testWidth != hotWireTest.Tests.Count) 
            { 
                throw new ArgumentException(); 
            }
            for (int i = 0; i < hotWireTest.Tests.Count; i++)
            {
                hotWireTest.Tests[i].Data.Add(new Point(double.Parse(values[i * HotWireReadWrite.testWidth + 1]), double.Parse(values[i * HotWireReadWrite.testWidth + 2]), Int32.Parse(values[i * HotWireReadWrite.testWidth])));
            }
            
        }

        #endregion
    }
}
