using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Graphics.Imaging;

using Microsoft.Edm;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ExcelServiceExplorer.Controls
{
    public sealed partial class ImageFromString : UserControl
    {
        private TaskScheduler ctx;

        public ImageFromString()
        {
            this.InitializeComponent();

            Loaded += ImageFromString_Loaded;
        }

        private void ImageFromString_Loaded(object sender, RoutedEventArgs e)
        {
            ctx = TaskScheduler.FromCurrentSynchronizationContext();
        }

        #region Properties
        // Source
        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set 
            {
                SetValue(SourceProperty, value);
                UpdateImage();
            }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ImageFromString), new PropertyMetadata(""));
        #endregion

        #region Events
        private async void UpdateImage()
        {
            if ((Source != null)  && (Source.GetType() == typeof(EdmString)) && (((EdmString)Source).Value != string.Empty))
            {
                var bitmapImage = new BitmapImage();
                byte[] byteBuffer = Convert.FromBase64String(((EdmString)Source).Value);

                var memoryStream = new MemoryStream(byteBuffer);
                memoryStream.Position = 0;

                await bitmapImage.SetSourceAsync(memoryStream.AsRandomAccessStream());
                Image.Source = bitmapImage;
            }
            else
            {
                Image.Source = null;
            }
        }
        #endregion
    }
}
