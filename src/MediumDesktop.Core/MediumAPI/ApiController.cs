using System.Threading.Tasks;
using DryIocAttributes;
using LiteDB;
using MediumDesktop.Core.Domain;
using MediumDesktop.Core.Managers.Interfaces;
using MediumSDK.WPF.Domain;

namespace MediumDesktop.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        private readonly IStoreManager _storeManager;
        private Token _accessToken;

        public ApiController(IStoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        public async Task<bool> AuthorizateAsync()  
        {
            var applicationData = await _storeManager.GetApplicationData();

            var oAuthClient = new OauthClient(applicationData.ClientId, applicationData.ClientSecret, "text");

            var code = await oAuthClient.GetAuthCode();

            var accessToken = await oAuthClient.GetToken(code);
            _accessToken = accessToken;

            return accessToken.AccessToken != null;
        }


        public async Task RefreshTokenAsync()
        {
        }

        public async Task GetUserProfile()
        {

        }
    }
}
