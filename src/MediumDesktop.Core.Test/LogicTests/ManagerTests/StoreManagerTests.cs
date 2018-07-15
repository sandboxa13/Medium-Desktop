using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using LiteDB;
using MediumDesktop.Core.Managers;
using Moq;
using Xunit;

namespace MediumDesktop.Core.Test.LogicTests.ManagerTests
{
    public class StoreManagerTests
    {
        private const string LocalFolder = "E:\\Repositories\\Medium-Desktop\\src\\MediumDesktop\\bin\\Debug\\";
        private readonly string _filePath = Path.Combine(LocalFolder, "MediumDesktop.db");

        [Fact]
        public async Task Get_Application_Data_Should_Return_Current_Application_Secret_And_Id()
        {
            var mockStoreManager = new Mock<StoreManager>(new LiteDatabase(_filePath)).Object;

            var result = await mockStoreManager.GetApplicationData();

            result.Should().NotBeNull();
            result.ClientSecret.Should().NotBeNullOrEmpty();
            result.ClientId.Should().NotBeNullOrEmpty();
        }
    }
}
