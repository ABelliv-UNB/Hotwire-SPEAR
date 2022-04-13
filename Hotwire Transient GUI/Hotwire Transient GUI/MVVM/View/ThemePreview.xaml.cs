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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotwire_Transient_GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for ThemePreview.xaml
    /// </summary>
    public partial class ThemePreview : UserControl
    {
        public List<ThemePreview> Group { get; set; }
        private string _ThemeName;
        public string ThemeName { 
            get 
            { 
                return _ThemeName;
            }
            set 
            {
                _ThemeName = value;
                ThemeNameBlock.Text = value;
            }
        }

        public ThemePreview()
        {
            InitializeComponent();
            IsSelected = false;
            this.MouseUp += ThemePreview_MouseUp;
        }

        private void ThemePreview_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
        }

        private Color _ThemeColor;
        public Color ThemeColor { 
            get { return _ThemeColor; }
            set
            {
                _ThemeColor = value;
                LeftMenu1.Background = new SolidColorBrush(value);
                LeftMenu2.Background = new SolidColorBrush(value);
            }
        }

        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (Group != null && value == true)
                {
                    foreach (ThemePreview t in Group)
                    {
                        t.IsSelected = false;
                    }
                }
                _IsSelected = value;
                if (value)
                {
                    SelectedBorder.Visibility = Visibility.Visible;
                    Application app = Application.Current;
                    app.Resources["MainColor"] = new SolidColorBrush(ThemeColor);
                    app.Resources["MainColorAsColor"] = ThemeColor;
                }
                else
                {
                    SelectedBorder.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
