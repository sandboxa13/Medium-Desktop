using System.Threading.Tasks;
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
            var configurationService = new ConfigurationService();  

            configurationService.SetBasePath("basePath");

            configurationService.BasePath.Should().Be("basePath");
        }   

        [Fact]
        public async Task Method_AddJSON_File_Should_Find_File_And_Deserialize()
        {   
            var configurationService = new ConfigurationService();

            configurationService.SetBasePath("../");
            await configurationService.AddJsonFile("appsettings.json");

            configurationService.GetValue<string>("ClientID").Should().NotBeNullOrEmpty();
        }
    }
}
