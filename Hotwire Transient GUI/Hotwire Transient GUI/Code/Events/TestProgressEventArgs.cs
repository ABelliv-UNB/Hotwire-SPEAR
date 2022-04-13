using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class TestProgressEventArgs
    {
        public int Completed { get; set; }
        public int Total { get; set; }

        public TestProgressEventArgs(int Completed, int Total)
        {
            this.Completed = Completed;
            this.Total = Total;
        }

    }
}
