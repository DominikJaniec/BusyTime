using System;
using System.Globalization;
using System.Windows.Data;

namespace BusyTime.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (targetType != typeof(string))
            {
                throw new ArgumentException("Can convert only to String", "targetType");
            }

            DateTime dayTime;
            if (value is DateTime)
            {
                dayTime = (DateTime)value;
            }
            else
            {
                throw new ArgumentException("Can convert only from DateTime", "value");
            }

            return dayTime.ToString(culture.DateTimeFormat.ShortTimePattern);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
