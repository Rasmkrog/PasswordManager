namespace PasswordManager.Core;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

public class ZeroToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int intValue && intValue == 0)
        {
            // ReSharper disable once HeapView.BoxingAllocation
            return Visibility.Collapsed;
        }

        // ReSharper disable once HeapView.BoxingAllocation
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
