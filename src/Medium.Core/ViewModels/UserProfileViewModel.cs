using System.Reactive.Disposables;
using DryIocAttributes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfileViewModel))]
    public class UserProfileViewModel : ReactiveObject, ISupportsActivation
    {
        [Reactive] public string Username { get; private set; }

        public ViewModelActivator Activator { get; }
        
        public UserProfileViewModel()
        {
            Activator = new ViewModelActivator();

            Username = "user user";
            
            this.WhenActivated(disposables =>
            {
                Disposable.Create(() => {}).DisposeWith(disposables);
            });
        }
    }
}