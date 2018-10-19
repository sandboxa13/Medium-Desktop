using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Medium.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            this.AttachDevTools();
        }

        private void InitializeComponent()
        {
        }
    }
}
