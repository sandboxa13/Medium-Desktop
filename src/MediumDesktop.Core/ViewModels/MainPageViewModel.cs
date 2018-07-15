using DryIocAttributes;
using MediumDesktop.Core.MediumAPI;
using MediumDesktop.Core.Services;
using MediumDesktop.Core.ViewModels.User;
using ReactiveUI.Fody.Helpers;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(MainPageViewModel))]
    public sealed class MainPageViewModel : BaseViewModel
    {
        private readonly IApiController _apiController;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(IApiController apiController, INavigationService navigationService)
        {
            _apiController = apiController;
            _navigationService = navigationService;

            UserContentViewModel = new UserContentViewModel(_apiController, _navigationService);
        }

        [Reactive] public UserContentViewModel UserContentViewModel { get; private set; }
    }
}
