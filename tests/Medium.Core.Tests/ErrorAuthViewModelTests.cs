using System;
using System.Threading.Tasks;
using Medium.Core.Interfaces;
using Medium.Core.ViewModels;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Medium.Core.Tests
{
    public class ErrorAuthViewModelTests
    {
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
        private readonly ITestOutputHelper _outputHelper;
        private readonly ErrorAuthViewModel _errorAuthViewModel;

        public ErrorAuthViewModelTests(ITestOutputHelper outputHelper)
        {
            _errorAuthViewModel = new ErrorAuthViewModel(_navigationService);    
        }
        
        [Fact]
        public async Task ShouldNavigateToLoginPageIfRetryButtonPressed()
        {
            _errorAuthViewModel.Activator.Activate();
            _errorAuthViewModel.RetryCommand.Execute().Subscribe();
            await Task.Delay(100);    
            
            _errorAuthViewModel.Activator.Deactivate();
            _navigationService.Received(1).Navigate(typeof(LoginViewModel));
        }
        
        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsNotActivated()
        {
            _errorAuthViewModel.RetryCommand.Execute().Subscribe();
            await Task.Delay(100);
            
            _navigationService.DidNotReceive().Navigate(Arg.Any<Type>());
        }

        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsDeactivated()
        {
            _errorAuthViewModel.Activator.Activate().Dispose();
            _errorAuthViewModel.RetryCommand.Execute().Subscribe();
            await Task.Delay(100);
            
            _navigationService.DidNotReceive().Navigate(Arg.Any<Type>());
        }
    }
}