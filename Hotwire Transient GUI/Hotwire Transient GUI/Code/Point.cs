using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code
{
    public class Point : IComparable<Point>
    {
        public int sample { get; set; }
        public double time { get; set; }
        public double wireTemp { get; set; }
        public double thermocouple { get; set; }
        public Point(double time, double wireTemp, int sampleNumber)
        {
            this.sample = sampleNumber;
            this.time = time;
            this.wireTemp = wireTemp;
        }
        public Point(double time, double wireTemp, double thermocouple, int sampleNumber)
        {
            this.sample = sampleNumber;
            this.time = time;
            this.wireTemp = wireTemp;
            this.thermocouple = thermocouple;
        }

        public int CompareTo(Point other)
        {
            if(other.time > this.time)
            {
                return -1;
            }
            else if(this.time == other.time)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }


}
