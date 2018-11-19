using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Services.MediumApi.Interfaces;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainWindowViewModel))]
    public sealed class MainWindowViewModel : ReactiveObject, ISupportsActivation
    {   
        [Reactive] public bool UserLoggedIn { get; private set; }
        [Reactive] public bool UserProfilePopUpIsOpen { get; private set; }
        [Reactive] public ISupportsActivation CurrentPage { get; private set; }
        [Reactive] public UserProfilePopUpViewModel UserProfilePopUpViewModel { get; private set; }
        [Reactive] public IEnumerable<ISupportsActivation> Pages { get; private set; }
        public ViewModelActivator Activator { get; }
            
        public ReactiveCommand<Unit, Unit> ShowPopUpCommand { get; }

        public MainWindowViewModel(     
            IAuthenticationManager authenticationManager,
            INavigationService navigationService,
            ErrorAuthViewModel errorAuthViewModel,
            MainPageViewModel mainPageViewModel,    
            LoginViewModel loginViewModel, 
            UserProfileViewModel userProfileViewModel,
            UserProfilePopUpViewModel userProfilePopUpViewModel)
        {
            Pages = new List<ISupportsActivation> { mainPageViewModel, loginViewModel, errorAuthViewModel, userProfileViewModel };

            ShowPopUpCommand = ReactiveCommand.Create(() => { });

            UserProfilePopUpViewModel = userProfilePopUpViewModel;
            
            Activator = new ViewModelActivator();
            
            this.WhenActivated(disposables =>
            {
                authenticationManager.LoggedIn()
                    .Subscribe(loggedIn => { UserLoggedIn = loggedIn; })
                    .DisposeWith(disposables);
                
                navigationService.CurrentPage()
                    .Select(type => Pages.First(x => x.GetType() == type))
                    .Subscribe(viewModel => CurrentPage = viewModel)
                    .DisposeWith(disposables);

                ShowPopUpCommand.Select(unit => UserProfilePopUpIsOpen = !UserProfilePopUpIsOpen)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }
    }
}
