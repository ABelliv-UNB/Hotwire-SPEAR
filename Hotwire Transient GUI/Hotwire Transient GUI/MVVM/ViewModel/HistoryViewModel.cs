using Hotwire_Transient_GUI.Code;
using Hotwire_Transient_GUI.Code.Events;
using Hotwire_Transient_GUI.Modals;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Hotwire_Transient_GUI.MVVM.ViewModel
{
	public class HistoryViewModel
	{

		public ObservableCollection<FileInfo> FilesInDirectory { get; set; }
		public FileInfo SelectedFile { get; set; }

		private string TestDirectory = @"C:\Users\abelliv1\Documents\Tests";
		public string SelectedDirectory { get; set; }

		public RelayCommand BrowseForTest { get; set; }
		public RelayCommand OpenTest { get; set; }
		public event EventHandler<NewTestEventArgs> NewTest;
		public event EventHandler<ShowDialogEventArgs> IOError;

		public HistoryViewModel()
		{
			BrowseForTest = new RelayCommand(o =>
			{
				BrowseCommand();
			});

			OpenTest = new RelayCommand(o =>
			{
				OpenTestCommand();
			});
			HotWireReadWrite.SelectedDirectory = TestDirectory;
		}

		public void updateFiles()
		{
			try
			{


				FilesInDirectory = new ObservableCollection<FileInfo>();
				SelectedDirectory = HotWireReadWrite.SelectedDirectory;
				string[] files = Directory.GetFiles(HotWireReadWrite.SelectedDirectory, "*.csv");

				foreach (string f in files)
				{
					FilesInDirectory.Add(new FileInfo(f));
				}
			}
			catch (Exception)
			{
				ShowError("Could not open default directory");
			}

		}

		private void BrowseCommand()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "(*.csv)|*.csv";
			openFileDialog.DefaultExt = "csv";
			if (openFileDialog.ShowDialog() ?? false)
			{
				OpenFile(openFileDialog.FileName);
			}
		}

		private void OpenTestCommand()
        {
			if(SelectedFile != null)
            {
				OpenFile(SelectedFile.FullName);
            }
        }


		private void OpenFile(string fileName)
        {
			try
			{
				HotWireTest hotWireTest = HotWireReadWrite.ReadTest(fileName);
				NewTestEventArgs args = new NewTestEventArgs(hotWireTest);
				OnNewTest(args);
			}
			catch (IOException)
			{
				ShowError("Could not open file. Make sure no other programs have it open");
			}
			catch (Exception)
			{
				ShowError("Could not open file");
			}
		}


		protected virtual void OnNewTest(NewTestEventArgs e)
		{
			EventHandler<NewTestEventArgs> handler = NewTest;
			handler?.Invoke(this, e);
		}

		private void ShowError(string Message)
        {
			OkModal dialog = new OkModal(Message);
			ShowDialogEventArgs args = new ShowDialogEventArgs(dialog);
			OnError(args);
		}

		protected virtual void OnError(ShowDialogEventArgs e)
		{
			EventHandler<ShowDialogEventArgs> handler = IOError;
			handler?.Invoke(this, e);
		}

	}
}

