using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class CalibrationEventArgs : EventArgs
    {
        public CalibrationReference CalibrationMaterial { get; set; }
        public double Tolerance { get; set;}

        public CalibrationEventArgs(CalibrationReference Material, double Tolerance)
        {
            CalibrationMaterial = Material;
            this.Tolerance = Tolerance;
        }
    }
}
