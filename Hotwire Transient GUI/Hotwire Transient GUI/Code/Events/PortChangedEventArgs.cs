using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class PortChangedEventArgs : EventArgs
    {
        public SerialPort Port { get; set; }

        public PortChangedEventArgs(SerialPort Port)
        {
            this.Port = Port;
        }
    }
}
