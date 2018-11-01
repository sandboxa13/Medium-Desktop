using Avalonia;
using Avalonia.Markup.Xaml;
using Medium.Core.ViewModels;
using ReactiveUI;

namespace Medium.Views
{
    public class MainPage : ReactiveUserControl<MainPageViewModel>
    {   
        public MainPage()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
