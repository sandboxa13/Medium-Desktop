using System;
using System.Globalization;
using System.Windows.Data;

namespace MediumDesktop.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return ((bool)value) ? VisibilityBoxes.CollapsedBox : VisibilityBoxes.VisibleBox;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return (bool)value
                ? BooleanBoxes.False
                : BooleanBoxes.True;
        }
    }
}
