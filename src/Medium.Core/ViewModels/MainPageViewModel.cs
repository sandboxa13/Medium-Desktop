using DryIocAttributes;
using Medium.Core.MediumAPI;
using Medium.Core.Services;

namespace Medium.Core.ViewModels
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

        }
    }
}
