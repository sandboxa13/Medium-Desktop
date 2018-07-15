using System.Threading.Tasks;
using DryIocAttributes;
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
        private OauthClient _oauthClient;

        public ApiController(IStoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        public async Task<bool> AuthorizateAsync()  
        {
            var applicationData = await _storeManager.GetApplicationData();

            _oauthClient = new OauthClient(applicationData.ClientId, applicationData.ClientSecret, "text");

            var code = await _oauthClient.GetAuthCode();

            var accessToken = await _oauthClient.GetToken(code);
            _accessToken = accessToken;

            await GetUserProfile();

            return accessToken.AccessToken != null;
        }


        public async Task RefreshTokenAsync()
        {
        }

        public async Task GetUserProfile()
        {
            _oauthClient.GetUserProfile();
        }
    }
}
