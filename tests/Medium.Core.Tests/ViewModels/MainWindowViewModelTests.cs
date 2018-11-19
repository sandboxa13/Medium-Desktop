using System;
using System.Threading.Tasks;
using FluentAssertions;
using Medium.Core.Interfaces;
using Medium.Core.ViewModels;
using Medium.Services.MediumApi.Interfaces;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Medium.Core.Tests.ViewModels
{
    public class MainWindowViewModelTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly IAuthenticationManager _authenticationManager = Substitute.For<IAuthenticationManager>();
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
        private readonly IUserProfileManager _userProfileManager = Substitute.For<IUserProfileManager>();
        private readonly MainWindowViewModel _mainWindowViewModel;


        public MainWindowViewModelTests(ITestOutputHelper testOutputHelper)
        {
            var errorAuthViewModel = new ErrorAuthViewModel(_navigationService);
            var mainPageViewModel = new MainPageViewModel(_navigationService);
            var loginViewModel = new LoginViewModel(_authenticationManager, _navigationService);
            var userProfileViewModel = new UserProfileViewModel();
            var userProfilePopUpViewModel = new UserProfilePopUpViewModel(_userProfileManager, _navigationService);

            _mainWindowViewModel = new MainWindowViewModel(
                _authenticationManager,
                _navigationService,
                errorAuthViewModel,
                mainPageViewModel,
                loginViewModel,
                userProfileViewModel,
                userProfilePopUpViewModel);
        }

        [Fact]
        public async Task ShouldChangeUserProfilePopUpVisibility()
        {
            _mainWindowViewModel.Activator.Activate();

            _mainWindowViewModel.UserProfilePopUpIsOpen.Should().Be(false);

            _mainWindowViewModel.ShowPopUpCommand.Execute().Subscribe();
            await Task.Delay(100);

            _mainWindowViewModel.UserProfilePopUpIsOpen.Should().Be(true);
        }

        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsNotActivated()
        {
            _mainWindowViewModel.ShowPopUpCommand.Execute().Subscribe();
            await Task.Delay(100);

            _mainWindowViewModel.UserProfilePopUpIsOpen.Should().Be(false);
        }

        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsDeactivated()
        {
            _mainWindowViewModel.Activator.Activate().Dispose();
            _mainWindowViewModel.ShowPopUpCommand.Execute().Subscribe();
            await Task.Delay(100);

            _mainWindowViewModel.UserProfilePopUpIsOpen.Should().Be(false);
        }
    }
}