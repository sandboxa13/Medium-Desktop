using Avalonia;
using Avalonia.Markup.Xaml;
using Medium.Core.ViewModels;
using ReactiveUI;

namespace Medium.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
