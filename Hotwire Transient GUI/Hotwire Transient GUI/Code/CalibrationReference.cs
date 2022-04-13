using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code
{
    public class CalibrationReference
    {
        public string Material { get; set; }
        public double ThermalConductivity { get; set; }
        public int RoomTemperature;

        public CalibrationReference() 
        {
            RoomTemperature = 20;
        }

        public CalibrationReference(string Material, double ThermalConductivity)
        {
            this.Material = Material;
            this.ThermalConductivity = ThermalConductivity;
            RoomTemperature = 20;
        }

        public CalibrationReference(string Material, double ThermalConductivity, int RoomTemp)
        {
            this.Material = Material;
            this.ThermalConductivity = ThermalConductivity;
            this.RoomTemperature = RoomTemp;
        }
    }
}
