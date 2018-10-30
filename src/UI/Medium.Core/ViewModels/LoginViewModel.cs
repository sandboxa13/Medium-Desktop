using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Managers;
using Medium.Services.Navigation;
using Medium.Services.Navigation.Navigation;
using MediumSDK.Net.Domain;
using ReactiveUI;

namespace Medium.Core.ViewModels
{

    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(AuthenticationViewModel))]
    public sealed class AuthenticationViewModel : ReactiveObject, IDisposable
    {   
        private MediumClient _mediumClient;

        private readonly IAuthenticationManager _authenticationManager;
        private readonly INavigationService _navigationService;

        public AuthenticationViewModel(
            IAuthenticationManager AuthenticationManager,
            INavigationService navigationService)
        {
            _authenticationManager = AuthenticationManager;
            _navigationService = navigationService;

            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                _mediumClient = new MediumClient(
                    "ce250fa7c114",
                    "bb152d21f43b20de5174495f488cd71aede8efaa",
                    "text");

                await _mediumClient.AuthenticateUser();
            });
        }

        public ReactiveCommand LoginCommand { get; }

        public void Dispose()
        {
        }

        private async void LoginHandler()
        {
           

            //await _AuthenticationManager.LoginAsync();

            //if (result)
            //{
            _navigationService.NavigateAsync(PageIndex.MainPage);
            //}
        }
    }
}
