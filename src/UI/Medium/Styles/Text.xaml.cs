using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Medium.Styles
{       
    public class Text : UserControl
    {
        public Text()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
