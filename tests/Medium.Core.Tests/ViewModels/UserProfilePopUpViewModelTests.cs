using System;
using System.Threading.Tasks;
using Medium.Core.Interfaces;
using Medium.Core.ViewModels;
using Medium.Services.MediumApi.Interfaces;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Medium.Core.Tests.ViewModels
{
    public class UserProfilePopUpViewModelTests
    {    
        private readonly ITestOutputHelper _outputHelper;
        private readonly IUserProfileManager _userProfileManager = Substitute.For<IUserProfileManager>();
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
        private readonly UserProfilePopUpViewModel _userProfilePopUpViewModel;

        public UserProfilePopUpViewModelTests(ITestOutputHelper testOutputHelper)
        {
            _userProfilePopUpViewModel = new UserProfilePopUpViewModel(_userProfileManager, _navigationService);
        }
        
        [Fact]
        public async Task ShouldNavigateToUserProfilePageIfRetryButtonPressed()
        {
            _userProfilePopUpViewModel.Activator.Activate();
            _userProfilePopUpViewModel.GoToUserProfileCommand.Execute().Subscribe();
            await Task.Delay(100);    
            
            _userProfilePopUpViewModel.Activator.Deactivate();
            _navigationService.Received(1).Navigate(typeof(UserProfileViewModel));
        }
        
        
        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsNotActivated()
        {
            _userProfilePopUpViewModel.GoToUserProfileCommand.Execute().Subscribe();
            await Task.Delay(100);
            
            _navigationService.DidNotReceive().Navigate(Arg.Any<Type>());
        }

        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsDeactivated()
        {
            _userProfilePopUpViewModel.Activator.Activate().Dispose();
            _userProfilePopUpViewModel.GoToUserProfileCommand.Execute().Subscribe();
            await Task.Delay(100);
            
            _navigationService.DidNotReceive().Navigate(Arg.Any<Type>());
        }
    }
}