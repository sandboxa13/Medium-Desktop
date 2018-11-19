using Medium.Core.Interfaces;
using Medium.Core.ViewModels;
using NSubstitute;
using Xunit.Abstractions;

namespace Medium.Core.Tests.ViewModels
{
    public class MainPageViewModelTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly MainPageViewModel _mainPageViewModel;
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();

        public MainPageViewModelTests(ITestOutputHelper testOutputHelper)
        {
            _mainPageViewModel = new MainPageViewModel(_navigationService);
        }
    }
}