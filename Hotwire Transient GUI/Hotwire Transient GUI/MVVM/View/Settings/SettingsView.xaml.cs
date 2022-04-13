using Hotwire_Transient_GUI;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Hotwire_Transient_GUI.Code;

namespace Hotwire_Transient_GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private List<ThemePreview> Group = new List<ThemePreview>();
        public SettingsView()
        {
            InitializeComponent();

            Application app = Application.Current;

            SolidColorBrush MainColor = (SolidColorBrush) app.Resources["MainColor"];
            foreach(ThemePreview t in ThemesPanel.Children)
            {
                Group.Add(t);
                if(t.ThemeColor == MainColor.Color)
                {
                    t.IsSelected = true;
                }
                t.Group = Group;
            }
        }

        private void Button_Browse(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                HotWireReadWrite.SelectedDirectory = dialog.FileName;
            }
        }
    }
}
