using System;
using System.Globalization;
using System.Windows.Data;

namespace BusyTime.Converter
{
    class RemainingTimeConverter : IValueConverter
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

            string result = "";
            string secondsAloneFormat = "{0:0.0} sec.";
            string secondsWithSthFormat = "{0:00.0} sec.";
            string secondsFomrat = secondsAloneFormat;

            if (remainingTime.Ticks < 0)
            {
                remainingTime = remainingTime.Negate();
            }

            if (remainingTime.TotalHours >= 1.0)
            {
                int hours = (int)remainingTime.TotalHours;
                result += String.Format("{0} godz. ", hours);
                secondsFomrat = secondsWithSthFormat;
            }

            if (remainingTime.TotalMinutes >= 1.0)
            {
                result += String.Format("{0} min. ", remainingTime.Minutes);
                secondsFomrat = secondsWithSthFormat;
            }

            return result += String.Format(secondsFomrat, remainingTime.Seconds + 0.001 * remainingTime.Milliseconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
