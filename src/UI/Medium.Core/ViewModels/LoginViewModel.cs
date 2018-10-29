using System;
using DryIocAttributes;
using Medium.Core.Managers;
using Medium.Services.Navigation;
using Medium.Services.Navigation.Navigation;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(AuthenticationViewModel))]
    public sealed class AuthenticationViewModel : ReactiveObject, IDisposable
    {
        private readonly IAuthenticationManager _AuthenticationManager;
        private readonly INavigationService _navigationService;

        public AuthenticationViewModel(
            IAuthenticationManager AuthenticationManager,
            INavigationService navigationService)
        {
            _AuthenticationManager = AuthenticationManager;
            _navigationService = navigationService;

            LoginCommand = ReactiveCommand.Create(LoginHandler);
        }

        public ReactiveCommand LoginCommand { get; }

        public void Dispose()
        {
        }

        private async void LoginHandler()
        {
            await _AuthenticationManager.LoginAsync();

            //if (result)
            //{
            _navigationService.NavigateAsync(PageIndex.MainPage);
            //}
        }
    }
}
