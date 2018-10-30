using System.Reactive.Disposables;
using DryIocAttributes;
using Medium.Services.Navigation;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(MainPageViewModel))]
    public sealed class MainPageViewModel : ReactiveObject, ISupportsActivation
    {
        public ViewModelActivator Activator { get; }
            
        public MainPageViewModel(
            //IMediumApiService mediumApiService, 
            INavigationService navigationService)
        {
            Activator = new ViewModelActivator();
            this.WhenActivated(disposables =>
            {
                // Example disposition logic.
                Disposable.Create(() => {}).DisposeWith(disposables);
            });
        }
    }
}
