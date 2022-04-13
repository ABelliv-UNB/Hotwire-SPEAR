using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class CalibrationDataEventArgs
    {
        public double Tolerance { get; set; }
        public SingleHotWireTest CalibraitonData { get; set; }

        public CalibrationDataEventArgs(SingleHotWireTest CalibrationData, double Tolerance = 5)
        {
            this.CalibraitonData = CalibrationData;
            this.Tolerance = Tolerance;
        }

    }
}
