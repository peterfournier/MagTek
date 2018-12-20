using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XFMagTek.Utilities
{
    public static class Converters
    {
        public static InverseBoolConverter InverseBoolConverter => new InverseBoolConverter();
        public static StringsAreEqualToBoolConverter StringsAreEqualToBoolConverter => new StringsAreEqualToBoolConverter();

    }

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    public class StringsAreEqualToBoolConverter : IValueConverter
    {
        public object Convert(object str1, Type targetType, object str2, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str1?.ToString()) || string.IsNullOrWhiteSpace(str2?.ToString()))
                return false;

            return str1.ToString().Equals(str2.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }
    }
}
