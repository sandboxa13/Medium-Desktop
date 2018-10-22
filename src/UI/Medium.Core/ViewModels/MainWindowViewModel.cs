using DryIocAttributes;
using Services.Interfaces.Interfaces;
using System;
using Medium.Domain.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainWindowViewModel))]
    public sealed class MainWindowViewModel : ReactiveObject, IDisposable
    {
        private readonly INavigationService _navigationService;
        private readonly IFactory<LoginViewModel> _loginFactory;

        public MainWindowViewModel(
            INavigationService navigationService,
            IFactory<LoginViewModel> loginFactory)
        {
            _navigationService = navigationService;
            _loginFactory = loginFactory;

            CurrentPageIndex = 0;

            LoginViewModel = _loginFactory.Create();

            InitSubscriptions();
        }

        [Reactive] public int CurrentPageIndex { get; set; }

        [Reactive] public LoginViewModel LoginViewModel { get; set; }

        private void InitSubscriptions()
        {
            _navigationService.CurrentPage()
                .Subscribe(index =>
            {
                switch (index)
                {
                    case PageIndex.AuthorizationPage:
                        CurrentPageIndex = 0;
                        break;

                    case PageIndex.MainPage:
                        CurrentPageIndex = 1;
                        break;

                    case PageIndex.SubscriptionsPage:
                        CurrentPageIndex = 2;
                        break;
                }
            });
        }

        public void Dispose()
        {
        }
    }
}
