using Avalonia;
using Avalonia.Markup.Xaml; 
using Medium.Core.ViewModels;
using ReactiveUI;

namespace Medium.Views  
{
    public class LoginPage : ReactiveUserControl<LoginViewModel>
    {
        public LoginPage()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
