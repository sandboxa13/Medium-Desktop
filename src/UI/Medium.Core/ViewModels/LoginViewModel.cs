using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Medium.Domain.Navigation;
using PropertyChanged;
using ReactiveUI;
using Services.Interfaces.Interfaces;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(LoginViewModel))]
    [AddINotifyPropertyChangedInterface]
    public sealed class LoginViewModel : ReactiveObject
    {
        private readonly ILoginManager _loginManager;
        private readonly INavigationService _navigationService;

        public LoginViewModel(
            ILoginManager loginManager,
            INavigationService navigationService)
        {
            _loginManager = loginManager;
            _navigationService = navigationService;

            LoginCommand = ReactiveCommand.Create(LoginHandler);
        }   

        public ReactiveCommand LoginCommand { get; }

        private async void LoginHandler()
        {
            var result = await _loginManager.LoginAsync();

            if (result)
            {
                _navigationService.NavigateAsync(PageIndex.MainPage);
            }
        }
    }
}
