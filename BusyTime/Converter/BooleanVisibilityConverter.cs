using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BusyTime.Converter
{
    class BooleanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (targetType != typeof(Visibility))
            {
                throw new ArgumentException("Can convert only to Visibility", "targetType");
            }

            bool showIt;
            if (value is bool)
            {
                showIt = (bool)value;

                if ("!".Equals(parameter))
                {
                    showIt = !showIt;
                }
            }
            else
            {
                throw new ArgumentException("Can convert only from Boolean", "value");
            }

            if (showIt)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
