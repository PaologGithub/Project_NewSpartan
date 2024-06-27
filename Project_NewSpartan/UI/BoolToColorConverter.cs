using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Project_NewSpartan.UI
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isImportant = (bool)value;
            if (isImportant)
            {
                return new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                var defaultTextColor = (SolidColorBrush)Application.Current.Resources["SystemControlForegroundBaseHighBrush"];
                return defaultTextColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
