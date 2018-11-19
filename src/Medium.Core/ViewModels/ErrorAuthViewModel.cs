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
    [ExportEx(typeof(ErrorAuthViewModel))]
    public sealed class ErrorAuthViewModel : ReactiveObject, ISupportsActivation
    {
        public ReactiveCommand<Unit, Unit> RetryCommand { get; }
        public ViewModelActivator Activator { get; }

        public ErrorAuthViewModel(INavigationService navigationService)
        {
            RetryCommand = ReactiveCommand.Create(() => {});
            
            Activator = new ViewModelActivator();
            
            this.WhenActivated(disposables =>
            {
                RetryCommand.Select(unit => typeof(LoginViewModel))
                    .Subscribe(navigationService.Navigate)
                    .DisposeWith(disposables);
                
                Disposable.Create(() => { }).DisposeWith(disposables);
            });
        }
    }
}
