using Hotwire_Transient_GUI.Code.Events;
using Hotwire_Transient_GUI.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Timers;
using System.IO.Ports;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
	public class NewTestViewModel : ObservableObject
	{
		public string Date
        {
			get { return DateTime.Now.ToString("yyyy-MM-dd"); }
        }
		public string Time
		{
			get { return DateTime.Now.ToString("HH:mm"); }
		}
		public string Material { get; set; }

		private Timer t;

		public event EventHandler<NewTestEventArgs> NewTest;
		public RelayCommand NewTestCommand { get; set; }
		public HotWireSerialCom PortManager { get; set; }
		public event EventHandler<ShowDialogEventArgs> ShowWindow;

		public NewTestViewModel(HotWireSerialCom PortManager)
        {
			initTimer();
			NewTestCommand = new RelayCommand(o =>
			{
				StartTest();
			});
			this.PortManager = PortManager;
			PortManager.ShowWindow += OnShowWindow;
			PortManager.NewTest += NewTestReceived;
		}

		public void StartTest()
        {
            HotWireTest newTest = new HotWireTest(10);
            newTest.SimulateTest();
            //ToDo:Some code happens here

            //PortControl.StartTest();

            NewTestEventArgs args = new NewTestEventArgs(newTest);
            OnNewTest(this, args);
        }

		public void NewTestReceived(object sender, NewTestEventArgs e)
		{
			OnNewTest(sender, e);
		}

		protected virtual void OnNewTest(object sender, NewTestEventArgs e)
        {
			EventHandler<NewTestEventArgs> handler = NewTest;
			handler?.Invoke(this, e);
		}


		private void initTimer()
		{ 
			t = new System.Timers.Timer();
			t.AutoReset = false;
			t.Elapsed += TimeChanged;
			t.Interval = GetInterval();
			t.Start();
		}

		private void TimeChanged(object sender, EventArgs e)
        {
			OnPropertyChanged("Date");
			OnPropertyChanged("Time");
			t.Interval = GetInterval();
			t.Start();
		}

		private double GetInterval()
		{
			DateTime now = DateTime.Now;
			return ((60 - now.Second) * 1000 - now.Millisecond);
		}

		protected virtual void OnShowWindow(object sender, ShowDialogEventArgs e)
		{
			EventHandler<ShowDialogEventArgs> handler = ShowWindow;
			handler?.Invoke(this, e);
		}
	}

}
