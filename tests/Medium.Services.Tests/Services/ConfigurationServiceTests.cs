using Moq;
using Services.Interfaces.Interfaces;
using Xunit;
using FluentAssertions;
using Services.Impl;


namespace Medium.Services.Tests.Services
{
    public class ConfigurationServiceTests
    {   
        [Fact]  
        public void Method_Set_Base_Path_Should_Set_Path_To_Specific_Directory()
        {
            var mockConfigurationService = /*new Mock<IConfigurationService>().Object;*/ new ConfigurationService();

            mockConfigurationService.SetBasePath("basePath");

            mockConfigurationService.BasePath.Should().Be("basePath");
        }
    }
}
