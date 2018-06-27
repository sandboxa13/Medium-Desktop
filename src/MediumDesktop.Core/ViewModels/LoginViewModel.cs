using DryIocAttributes;
using MediumDesktop.Core.Managers.Interfaces;
using MediumDesktop.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(LoginViewModel))]
    public sealed class LoginViewModel 
    {   
        private readonly ILoginManager _loginManager;
        private readonly INavigationService _navigationService;

        public LoginViewModel(ILoginManager loginManager, INavigationService navigationService)
        {
            _loginManager = loginManager;
            _navigationService = navigationService;

            _navigationService.Navigate<MainPageViewModel>();

            //LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            //{
            //    await _loginManager.LoginAsync(Username, Password);
            //});
        }

       
        [Reactive] public string Username { get; set; }

        [Reactive] public string Password { get; set; }

        public ReactiveCommand LoginCommand { get; }
    }
}
