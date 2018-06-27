using MediumDesktop.Core.Managers.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MediumDesktop.Core.ViewModels
{
    public sealed class LoginViewModel : ViewModelBase
    {
        private readonly ILoginManager _loginManager;

        public LoginViewModel(ILoginManager loginManager)
        {
            _loginManager = loginManager;

            LoginCommand = ReactiveCommand.Create(async () =>   
            {
                await _loginManager.LoginAsync(Username, Password);
            });
        }

       
        [Reactive] public string Username { get; set; }

        [Reactive] public string Password { get; set; }

        public ReactiveCommand LoginCommand { get; }
    }
}
