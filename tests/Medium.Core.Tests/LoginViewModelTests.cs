using System;
using System.Threading.Tasks;
using Medium.Core.Interfaces;
using Medium.Core.ViewModels;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Medium.Core.Tests
{
    public class LoginViewModelTests
    {
        private readonly IAuthenticationManager _authenticationManager = Substitute.For<IAuthenticationManager>();
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
        private readonly ITestOutputHelper _outputHelper;
        private readonly LoginViewModel _loginViewModel;
        
        public LoginViewModelTests(ITestOutputHelper outputHelper)
        {
            _loginViewModel = new LoginViewModel(_authenticationManager, _navigationService);
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task ShouldNavigateToMainPageIfAuthenticationSucceeded()
        {
            _loginViewModel.Activator.Activate();
            _loginViewModel.LoginCommand.Execute().Subscribe();
            await Task.Delay(100);    
            
            _loginViewModel.Activator.Deactivate();
            _navigationService.Received(1).Navigate(typeof(MainPageViewModel));
        }

        [Fact]
        public async Task ShouldNavigateToErrorPageIfAuthenticationFailed()
        {
            _loginViewModel.Activator.Activate();
            _authenticationManager.LoginAsync().ReturnsForAnyArgs(x => throw new Exception());
            _loginViewModel.LoginCommand.Execute().Subscribe(x => { }, error => { /* Need this to avoid exn */ });
            await Task.Delay(100);
            
            _loginViewModel.Activator.Deactivate();
            _navigationService.Received(1).Navigate(typeof(ErrorAuthViewModel));
        }

        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsNotActivated()
        {
            _loginViewModel.LoginCommand.Execute().Subscribe();
            await Task.Delay(100);
            
            _navigationService.DidNotReceive().Navigate(Arg.Any<Type>());
        }

        [Fact]
        public async Task ShouldDoNothingWhenViewModelIsDeactivated()
        {
            _loginViewModel.Activator.Activate().Dispose();
            _loginViewModel.LoginCommand.Execute().Subscribe();
            await Task.Delay(100);
            
            _navigationService.DidNotReceive().Navigate(Arg.Any<Type>());
        }
    }
}
