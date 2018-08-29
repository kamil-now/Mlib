using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Mlib.UI.Converters
{
    public class BoolToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (parameter is bool)
                    return (bool)value == (bool)parameter ? Visibility.Visible : Visibility.Collapsed;
                else
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
