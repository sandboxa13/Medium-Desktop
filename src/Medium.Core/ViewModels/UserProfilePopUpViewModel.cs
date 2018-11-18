using DryIocAttributes;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfilePopUpViewModel))]
    public class UserProfilePopUpViewModel : ReactiveObject, ISupportsActivation
    {
        public ViewModelActivator Activator { get; }

        public UserProfilePopUpViewModel()
        {
            Activator = new ViewModelActivator();

            //this.WhenActivated(disposable =>
            //{

            //});
        }
    }
}
