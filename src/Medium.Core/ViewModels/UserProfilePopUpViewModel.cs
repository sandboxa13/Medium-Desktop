using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Services.MediumApi.Interfaces;
using MediumSDK.Net.Domain;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfilePopUpViewModel))]
    public class UserProfilePopUpViewModel : ReactiveObject, ISupportsActivation
    {        
        public ReactiveCommand<Unit, MediumUser> GoToUserProfileCommand { get; }

        public ViewModelActivator Activator { get; }

        public UserProfilePopUpViewModel(
            IUserProfileManager userProfileManager, 
            INavigationService navigationService)
        {
            GoToUserProfileCommand = ReactiveCommand.CreateFromTask(userProfileManager.GetUser);

            Activator = new ViewModelActivator();
            
            this.WhenActivated(disposables =>
            {
                GoToUserProfileCommand.Select(user => typeof(UserProfileViewModel))
                    .Subscribe(navigationService.Navigate)
                    .DisposeWith(disposables);
                
                Disposable.Create(() => { }).DisposeWith(disposables);
            });
        }
    }
}
