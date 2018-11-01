using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DryIocAttributes;
using Medium.Core.Domain;
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
        public ReactiveCommand<Unit, AuthResult> LoginCommand { get; }
        public ViewModelActivator Activator { get; }

        public LoginViewModel(
            IAuthenticationManager authenticationManager,
            INavigationService navigationService)
        {
            Activator = new ViewModelActivator();

            LoginCommand = ReactiveCommand.CreateFromTask(authenticationManager.LoginAsync);

            this.WhenActivated(disposables =>
            {   
                LoginCommand.Where(result => result == AuthResult.Succses)
                    .Select(ignore => PageIndex.MainPage)
                    .Subscribe(navigationService.NavigateAsync)
                    .DisposeWith(disposables);

                LoginCommand.Where(result => result == AuthResult.Error)
                    .Select(result => PageIndex.ErrorAuthPage)
                    .Subscribe(navigationService.NavigateAsync)
                    .DisposeWith(disposables);
            });
        }
    }
}
