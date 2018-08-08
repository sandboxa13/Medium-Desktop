using DryIocAttributes;
using Medium.Core.Services;
using Medium.Core.ViewModels.User;
using ReactiveUI.Fody.Helpers;
using Services.Interfaces.Interfaces;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainWindowViewModel))]
    public sealed class MainWindowViewModel : BaseViewModel
    {
        private readonly IMediumApiService _mediumApiService;
        private readonly INavigationService _navigationService;

        public MainWindowViewModel(IMediumApiService mediumApiService, INavigationService navigationService)
        {
            _mediumApiService = mediumApiService;
            _navigationService = navigationService;

            UserContentViewModel = new UserContentViewModel(_mediumApiService, _navigationService);
        }

        [Reactive] public UserContentViewModel UserContentViewModel { get; private set; }
    }
}
