using System.Windows;

namespace MediumDesktop.Converters
{
    internal static class VisibilityBoxes
    {
        public static readonly object VisibleBox = Visibility.Visible;
        public static readonly object HiddenBox = Visibility.Hidden;
        public static readonly object CollapsedBox = Visibility.Collapsed;

        public static object Box(Visibility value)
        {
            switch (value)
            {
                case Visibility.Visible:
                    return VisibleBox;
                case Visibility.Hidden:
                    return HiddenBox;
            }
            return CollapsedBox;
        }
    }

}
