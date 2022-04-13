using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class SelectedButtonChangedEventArgs
    {
        public string Button { get; set; }
        public bool IsSelected { get; set; }

        public SelectedButtonChangedEventArgs(string button, bool isSleceted)
        {
            Button = button;
            IsSelected = isSleceted;
        }
    }
}
