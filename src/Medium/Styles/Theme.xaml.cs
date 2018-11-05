using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Medium.Styles     
{       
    public class Theme : UserControl
    {
        public Theme()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
