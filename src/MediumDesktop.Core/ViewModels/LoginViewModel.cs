using System.Threading.Tasks;
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
        public LoginViewModel(
            ILoginManager loginManager,
            INavigationService navigationService)
        {
            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
                {
                    await navigationService.NavigateAsync<MainPageViewModel>();
                });
        }


        [Reactive] public string Username { get; set; }

        [Reactive] public string Password { get; set; }

        public ReactiveCommand LoginCommand { get; set; }
    }
}
