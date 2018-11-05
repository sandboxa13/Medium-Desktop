using Avalonia;
using Avalonia.Markup.Xaml;
using Medium.Core.ViewModels;
using ReactiveUI;

namespace Medium.Views
{
    public class ErrorAuthPage : ReactiveUserControl<ErrorAuthViewModel>
    {   
        public ErrorAuthPage()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
