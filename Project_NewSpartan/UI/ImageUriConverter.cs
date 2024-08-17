using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Project_NewSpartan.UI
{
    public class ImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string url = value as string;
            if (string.IsNullOrEmpty(url))
                return null;

            return new BitmapImage(new Uri(url));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
