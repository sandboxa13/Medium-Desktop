using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Medium.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(LoginViewModel))]
    public sealed class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(
            ILoginManager loginManager,
            INavigationService navigationService,
            IMainWindowService mainWindowService)
        {
            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                SpinnerVisible = true;

                var result = await loginManager.LoginAsync();

                if (result)
                {
                    mainWindowService.ActivateWindow();
                    await navigationService.NavigateAsync<MainPageViewModel>();

                    SpinnerVisible = false;
                }
            });
        }
            
        public ReactiveCommand LoginCommand { get; }

        [Reactive] public bool SpinnerVisible { get; set; }

        [Reactive] public string TestText { get; set; }
    }
}
