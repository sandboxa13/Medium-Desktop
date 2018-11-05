using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DryIocAttributes;
using Medium.Core.Interfaces;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]    
    [ExportEx(typeof(LoginViewModel))]
    public sealed class LoginViewModel : ReactiveObject, ISupportsActivation
    {
        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ViewModelActivator Activator { get; }

        public LoginViewModel(  
            IAuthenticationManager authenticationManager,
            INavigationService navigationService)
        {
            LoginCommand = ReactiveCommand.CreateFromTask(authenticationManager.LoginAsync);

            Activator = new ViewModelActivator();
            this.WhenActivated(disposables =>
            {    
                LoginCommand.Select(unit => typeof(MainPageViewModel))
                    .Subscribe(navigationService.Navigate)
                    .DisposeWith(disposables);

                LoginCommand.ThrownExceptions
                    .Select(exception => typeof(ErrorAuthViewModel))
                    .Subscribe(navigationService.Navigate)
                    .DisposeWith(disposables);
            });
        }
    }
}
