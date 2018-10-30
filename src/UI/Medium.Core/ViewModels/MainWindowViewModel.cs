using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Medium.Services.Navigation;
using Medium.Services.Navigation.Navigation;
using Medium.Services.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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
            IFactory<LoginViewModel> authorizationFactory,
            IFactory<MainPageViewModel> mainPageFactory,
            INavigationService navigationService)
        {
            var pages = new List<ISupportsActivation>
            {
                authorizationFactory.Create(),
                mainPageFactory.Create()
            };
            Pages = pages;
            
            var typeMap = new Dictionary<PageIndex, Type>
            {
                {PageIndex.AuthenticationPage, typeof(LoginViewModel)},
                {PageIndex.MainPage, typeof(MainPageViewModel)},
                {PageIndex.SubscriptionsPage, typeof(object)}
            };

            Activator = new ViewModelActivator();
            this.WhenActivated(disposables =>
            {
                navigationService.CurrentPage()
                    .Select(index => typeMap[index])
                    .Select(type => Pages.First(x => x.GetType() == type))
                    .Subscribe(viewModel => CurrentPage = viewModel)

                    .DisposeWith(disposables);
            });
        }
    }
}
