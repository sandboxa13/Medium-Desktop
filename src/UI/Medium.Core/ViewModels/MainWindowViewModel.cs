using DryIocAttributes;
using System;
using Medium.Domain.Navigation;
using Medium.Services.Navigation;
using Medium.Services.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainWindowViewModel))]
    public sealed class MainWindowViewModel : ReactiveObject, IDisposable
    {
        private readonly INavigationService _navigationService; 
        private readonly IFactory<AuthorizationViewModel> _authorizationFactory;
        private readonly IFactory<MainPageViewModel> _mainPageFactory;

        public MainWindowViewModel(     
            INavigationService navigationService,
            IFactory<AuthorizationViewModel> authorizationFactory,
            IFactory<MainPageViewModel> mainPageFactory)
        {
            _navigationService = navigationService;
            _authorizationFactory = authorizationFactory;
            _mainPageFactory = mainPageFactory;

            InitSubscriptions();
        }
            
        [Reactive] public int CurrentPageIndex { get; private set; }
        [Reactive] public AuthorizationViewModel AuthorizationViewModel { get; private set; }
        [Reactive] public MainPageViewModel MainPageViewModel { get; private set; }

        public void Dispose()
        {
            AuthorizationViewModel?.Dispose();
            MainPageViewModel?.Dispose();
        }

        private void InitSubscriptions()
        {
            _navigationService.CurrentPage()
                .Subscribe(index =>
            {
                switch (index)
                {
                    case PageIndex.AuthorizationPage:
                        GoToAuthorizationPage();
                        break;

                    case PageIndex.MainPage:
                        GoToMainPage();
                        break;

                    case PageIndex.SubscriptionsPage:
                        CurrentPageIndex = 2;
                        break;
                }
            });

            GoToAuthorizationPage();
        }
            
        private void GoToAuthorizationPage()
        {
            AuthorizationViewModel = _authorizationFactory.Create();
            CurrentPageIndex = 0;
        }

        private void GoToMainPage()
        {
            AuthorizationViewModel?.Dispose();
            MainPageViewModel = _mainPageFactory.Create();
            CurrentPageIndex = 1;
        }
    }
}
