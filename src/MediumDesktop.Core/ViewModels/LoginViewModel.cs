using DryIocAttributes;
using MediumDesktop.Core.Managers.Interfaces;
using MediumDesktop.Core.Services;
using ReactiveUI;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(LoginViewModel))]
    public sealed class LoginViewModel
    {
        public LoginViewModel(
            ILoginManager loginManager,
            INavigationService navigationService,
            IMainWindowService mainWindowService)
        {
            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await loginManager.LoginAsync();

                if (result)
                {
                    mainWindowService.ActivateWindow();
                    await navigationService.NavigateAsync<MainPageViewModel>();
                }
            });
        }

        public ReactiveCommand LoginCommand { get; set; }
    }
}
