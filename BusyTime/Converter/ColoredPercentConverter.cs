using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BusyTime.Converter
{
    class ColoredPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            double percent;
            if (value is double)
            {
                percent = (double)value;
            }
            else
            {
                throw new ArgumentException("Can convert only from Double", "value");
            }

            if (percent < 0.25)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if (percent < 0.5)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            else if (percent < 1.0)
            {
                return new SolidColorBrush(Colors.Green);
            }
            else
            {
                return new SolidColorBrush(Colors.Teal);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
