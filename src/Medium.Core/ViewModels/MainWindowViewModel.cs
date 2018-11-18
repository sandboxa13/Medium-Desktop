using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DryIocAttributes;
using Medium.Core.Interfaces;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainWindowViewModel))]
    public sealed class MainWindowViewModel : ReactiveObject, ISupportsActivation
    {
        [Reactive] 
        public ISupportsActivation CurrentPage { get; private set; }
        
        public IEnumerable<ISupportsActivation> Pages { get; }
        public ViewModelActivator Activator { get; }
            
        public MainWindowViewModel(     
            INavigationService navigationService,
            ErrorAuthViewModel errorAuthViewModel,
            MainPageViewModel mainPageViewModel,
            LoginViewModel loginViewModel, 
            UserProfilePopUpViewModel userProfilePopUpViewModel)
        {
            Pages = new List<ISupportsActivation> { mainPageViewModel, loginViewModel, errorAuthViewModel };

            Activator = new ViewModelActivator();
            this.WhenActivated(disposables =>
            {
                navigationService.CurrentPage()
                    .Select(type => Pages.First(x => x.GetType() == type))
                    .Subscribe(viewModel => CurrentPage = viewModel)
                    .DisposeWith(disposables);
            });
        }
    }
}
