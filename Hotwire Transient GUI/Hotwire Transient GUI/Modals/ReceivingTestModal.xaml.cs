using Hotwire_Transient_GUI.Code.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotwire_Transient_GUI.Modals
{
    /// <summary>
    /// Interaction logic for ReceivingTestModal.xaml
    /// </summary>
    public partial class ReceivingTestModal : Window
    {
        public int TotalTests { get; set; }
        public int CompletedTests { get; set; }
        public ReceivingTestModal()
        {
            InitializeComponent();
        }

        public void TestUpdated(object sender, TestProgressEventArgs e)
        {
            if(e.Completed == -1 || e.Total == -1)
            {
                this.Close();
            }
            else
            {
                CompletedTests = e.Completed;
                TotalTests = e.Total;
                TestProgressText.Text = "(" + CompletedTests + "/" + TotalTests + ")";
                progressBar.Maximum = TotalTests;
                progressBar.Value = CompletedTests;
            }
        }
    }
}
