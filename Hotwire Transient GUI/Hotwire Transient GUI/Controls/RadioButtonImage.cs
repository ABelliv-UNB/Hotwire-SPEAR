using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hotwire_Transient_GUI.Controls
{
    class RadioButtonImage : RadioButton
    {
        public string MainImage
        {
            get { return (string)GetValue(MainImageProperty); }
            set { SetValue(MainImageProperty, value);
                ShownImage = new BitmapImage(new Uri(MainImage));
            }
        }
        public static readonly DependencyProperty MainImageProperty =
          DependencyProperty.Register("MainImage", typeof(string), typeof(RadioButton), new UIPropertyMetadata(default(string)));


        public string SelectedImage
        {
            get { return (string)GetValue(SelectedImageProperty); }
            set { SetValue(SelectedImageProperty, value);
                ShownImage = new BitmapImage(new Uri(SelectedImage));  }
        }
        public static readonly DependencyProperty SelectedImageProperty =
          DependencyProperty.Register("SelectedImage", typeof(string), typeof(RadioButton), new UIPropertyMetadata(default(string)));

        public ImageSource ShownImage
        {
            get { return (ImageSource)GetValue(ShownImageProperty); }
            set { SetValue(ShownImageProperty, value); }
        }
        public static readonly DependencyProperty ShownImageProperty =
          DependencyProperty.Register("ShownImage", typeof(ImageSource), typeof(RadioButton), new UIPropertyMetadata(default(ImageSource)));



    }
}

