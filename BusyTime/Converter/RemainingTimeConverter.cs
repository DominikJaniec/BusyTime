using System;
using System.Globalization;
using System.Windows.Data;

namespace BusyTime.Converter
{
    public class RemainingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            TimeSpan remainingTime;
            if (value is TimeSpan)
            {
                remainingTime = (TimeSpan)value;
            }
            else
            {
                throw new ArgumentException("Can convert only from TimeSpan", "value");
            }

            string result;

            if (remainingTime.Ticks >= 0)
            {
                result = "Pozostało jeszcze";
            }
            else
            {
                remainingTime = remainingTime.Negate();
                result = "Już ponad";
            }

            if (remainingTime.TotalHours >= 1.0)
            {
                int hours = (int)remainingTime.TotalHours;
                result = String.Format(" {0} godz.", hours);
            }

            result += String.Format(" {0} min.", remainingTime.Minutes);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
