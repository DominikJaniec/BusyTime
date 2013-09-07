﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace BusyTime.Converter
{
    public class TimeSpanConverter : IValueConverter
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

            TimeSpan timeSpan;
            if (value is TimeSpan)
            {
                timeSpan = (TimeSpan)value;
            }
            else
            {
                throw new ArgumentException("Can convert only from TimeSpan", "value");
            }

            return String.Format("{0}{1}{2}", (int)timeSpan.TotalHours, culture.DateTimeFormat.TimeSeparator, timeSpan.Minutes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
