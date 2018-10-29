using DryIocAttributes;
using System;
using Medium.Services.Navigation;
using Medium.Services.Navigation.Navigation;
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
        private readonly IFactory<AuthenticationViewModel> _AuthenticationFactory;
        private readonly IFactory<MainPageViewModel> _mainPageFactory;

        public MainWindowViewModel(     
            INavigationService navigationService,
            IFactory<AuthenticationViewModel> AuthenticationFactory,
            IFactory<MainPageViewModel> mainPageFactory)
        {
            _navigationService = navigationService;
            _AuthenticationFactory = AuthenticationFactory;
            _mainPageFactory = mainPageFactory;

            InitSubscriptions();
        }
            
        [Reactive] public int CurrentPageIndex { get; private set; }
        [Reactive] public AuthenticationViewModel AuthenticationViewModel { get; private set; }
        [Reactive] public MainPageViewModel MainPageViewModel { get; private set; }

        public void Dispose()
        {
            AuthenticationViewModel?.Dispose();
            MainPageViewModel?.Dispose();
        }

        private void InitSubscriptions()
        {
            _navigationService.CurrentPage()
                .Subscribe(CurrentPageChangedHandler());

            GoToAuthenticationPage();
        }
       

        private void GoToAuthenticationPage()
        {
            AuthenticationViewModel = _AuthenticationFactory.Create();
            CurrentPageIndex = 0;
        }

        private void GoToMainPage()
        {
            AuthenticationViewModel?.Dispose();
            MainPageViewModel = _mainPageFactory.Create();
            CurrentPageIndex = 1;
        }

        private Action<PageIndex> CurrentPageChangedHandler()
        {
            return index =>
            {
                switch (index)
                {
                    case PageIndex.AuthenticationPage:
                        GoToAuthenticationPage();
                        break;

                    case PageIndex.MainPage:
                        GoToMainPage();
                        break;

                    case PageIndex.SubscriptionsPage:
                        CurrentPageIndex = 2;
                        break;
                }
            };
        }
    }
}
