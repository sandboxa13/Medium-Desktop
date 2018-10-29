using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Domain.Navigation;
using Medium.Services.Navigation;
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
            IAuthorizationManager authorizationManager,
            INavigationService navigationService)
        {   
            Activator = new ViewModelActivator();
            LoginCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(true));
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
