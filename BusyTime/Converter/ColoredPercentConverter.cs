using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BusyTime.Converter
{
    class ColoredPercentConverter : IValueConverter
    {
        private static readonly Lazy<SolidColorBrush> colorRed = new Lazy<SolidColorBrush>(() => new SolidColorBrush(Colors.Red));
        private static readonly Lazy<SolidColorBrush> colorYellow = new Lazy<SolidColorBrush>(() => new SolidColorBrush(Colors.Yellow));
        private static readonly Lazy<SolidColorBrush> colorGreen = new Lazy<SolidColorBrush>(() => new SolidColorBrush(Colors.Green));
        private static readonly Lazy<SolidColorBrush> colorTeal = new Lazy<SolidColorBrush>(() => new SolidColorBrush(Colors.Teal));

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
                return colorRed.Value;
            }
            else if (percent < 0.5)
            {
                return colorYellow.Value;
            }
            else if (percent < 1.0)
            {
                return colorGreen.Value;
            }
            else
            {
                return colorTeal.Value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
