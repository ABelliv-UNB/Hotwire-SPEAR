using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class NewTestEventArgs : EventArgs
    {
        public HotWireTest HotWireTest { get; set; }

        public NewTestEventArgs(HotWireTest newTest)
        {
            HotWireTest = newTest;
        }
    }
}
