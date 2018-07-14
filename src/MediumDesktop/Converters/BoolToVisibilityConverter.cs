using System;
using System.Globalization;
using System.Windows.Data;

namespace MediumDesktop.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return ((bool)value ^ Inverse) ? VisibilityBoxes.VisibleBox : VisibilityBoxes.CollapsedBox;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
