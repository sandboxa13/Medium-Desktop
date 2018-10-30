using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Services.Navigation;
using Medium.Services.Navigation.Navigation;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]    
    [ExportEx(typeof(LoginViewModel))]
    public sealed class LoginViewModel : ReactiveObject, ISupportsActivation
    {
        public ReactiveCommand<Unit, bool> LoginCommand { get; }
        public ViewModelActivator Activator { get; }

        public LoginViewModel(
            IAuthenticationManager authenticationManager,
            INavigationService navigationService)
        {
            Activator = new ViewModelActivator();

            LoginCommand = ReactiveCommand.CreateFromTask(authenticationManager.LoginAsync, outputScheduler: RxApp.TaskpoolScheduler);

            this.WhenActivated(disposables =>
            {
                LoginCommand.Where(loggedIn => loggedIn)
                    .Select(ignore => PageIndex.MainPage)
                    .Subscribe(navigationService.NavigateAsync)
                    .DisposeWith(disposables);
            });
        }
    }
}
