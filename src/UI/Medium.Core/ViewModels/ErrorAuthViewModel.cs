using System.Reactive.Disposables;
using DryIocAttributes;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(ErrorAuthViewModel))]
    public class ErrorAuthViewModel : ReactiveObject, ISupportsActivation
    {
        public ViewModelActivator Activator { get; }

        public ErrorAuthViewModel()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated(disposables =>
            {

                Disposable.Create(() => { }).DisposeWith(disposables);
            });
        }
    }
}
