using System;
using DryIocAttributes;
using Medium.Services.MediumApi;
using Medium.Services.Navigation;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(MainPageViewModel))]
    public sealed class MainPageViewModel : ReactiveObject, IDisposable
    {
        private readonly IMediumApiService _mediumApiService;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(IMediumApiService mediumApiService, INavigationService navigationService)
        {
            _mediumApiService = mediumApiService;
            _navigationService = navigationService;
        }

        public void Dispose()
        {
        }
    }
}
