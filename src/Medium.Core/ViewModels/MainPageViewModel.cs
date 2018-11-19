using System.Reactive.Disposables;
using DryIocAttributes;
using Medium.Core.Interfaces;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainPageViewModel))]
    public sealed class MainPageViewModel : ReactiveObject, ISupportsActivation
    {
        public ViewModelActivator Activator { get; }
        public MainPageViewModel(
            INavigationService navigationService)
        {
            Activator = new ViewModelActivator();
            
            this.WhenActivated(disposables => { Disposable.Create(() => { }).DisposeWith(disposables); });
        }
    }
}