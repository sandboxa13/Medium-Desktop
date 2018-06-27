using DryIocAttributes;
using MediumDesktop.Core.Managers.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(LoginViewModel))]
    public sealed class LoginViewModel : ViewModelBase
    {
        private readonly ILoginManager _loginManager;

        public LoginViewModel(ILoginManager loginManager)
        {
            _loginManager = loginManager;

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
