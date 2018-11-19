using System;
using System.Threading.Tasks;
using FluentAssertions;
using Medium.Core.Managers;
using Medium.Core.ViewModels;
using Xunit;
using Xunit.Abstractions;

namespace Medium.Core.Tests.Managers
{
    public class NavigationServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly NavigationService _navigationService;

        public NavigationServiceTests(ITestOutputHelper testOutputHelper)
        {
            _navigationService = new NavigationService();
        }

        [Fact]
        public async Task ShouldDoNavigateToSpecificPage()
        {
            _navigationService.Navigate(typeof(MainPageViewModel));
            await Task.Delay(100);

            _navigationService.CurrentPage().Subscribe(type => { type.Should().Be(typeof(MainPageViewModel)); });
        }
    }
}