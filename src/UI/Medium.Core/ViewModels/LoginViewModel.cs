using System;
using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Medium.Services.Navigation;
using Medium.Services.Navigation.Navigation;
using ReactiveUI;

namespace Medium.Core.ViewModels    
{
    [Reuse(ReuseType.Transient)]    
    [ExportEx(typeof(AuthorizationViewModel))]
    public sealed class AuthorizationViewModel : ReactiveObject, IDisposable
    {
        private readonly IAuthorizationManager _authorizationManager;
        private readonly INavigationService _navigationService;

        public AuthorizationViewModel(
            IAuthorizationManager authorizationManager,
            INavigationService navigationService)
        {   
            _authorizationManager = authorizationManager;
            _navigationService = navigationService;

            LoginCommand = ReactiveCommand.Create(LoginHandler);
        }   

        public ReactiveCommand LoginCommand { get; }

        public void Dispose()
        {
        }

        private async void LoginHandler()
        {
            var result = await _authorizationManager.LoginAsync();

            if (result)
            {
                _navigationService.NavigateAsync(PageIndex.MainPage);
            }
        }
    }
}
