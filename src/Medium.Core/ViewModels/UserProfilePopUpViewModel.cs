using DryIocAttributes;
using Medium.Services.MediumApi.Interfaces;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfilePopUpViewModel))]
    public class UserProfilePopUpViewModel : ReactiveObject, ISupportsActivation
    {
        private readonly IUserProfileManager _userProfileManager;

        public ViewModelActivator Activator { get; }

        public UserProfilePopUpViewModel(
            IUserProfileManager userProfileManager)
        {
            _userProfileManager = userProfileManager;
            Activator = new ViewModelActivator();

            //this.WhenActivated(disposable =>
            //{
            // 
            //});
        }
    }
}
