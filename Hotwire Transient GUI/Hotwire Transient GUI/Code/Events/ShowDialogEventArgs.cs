using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotwire_Transient_GUI.Code.Events
{
    public class ShowDialogEventArgs : EventArgs
    {
        public Window Dialog { get; set; }
        public bool WaitUntilClose { get; set; }

        public ShowDialogEventArgs(Window Dialog, bool WaitUntilClose = true)
        {
            this.Dialog = Dialog;
            this.WaitUntilClose = WaitUntilClose;
        }
    }
}
