using Avalonia;
using Avalonia.Markup.Xaml;
using Medium.Core.ViewModels;
using ReactiveUI;

namespace Medium.Views
{
    public class UserProfilePopUp : ReactiveWindow<UserProfilePopUpViewModel>
    {
        public UserProfilePopUp()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
