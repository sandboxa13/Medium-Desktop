using System.Threading.Tasks;
using DryIocAttributes;
using Medium.SDK.Domain;

namespace Medium.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        private OauthClient _oauthClient;

        public ApiController()
        {
        }

        public async Task<bool> AuthorizateAsync()  
        {
            _oauthClient = new OauthClient("", "", "text");

            var code = await _oauthClient.GetAuthCode();

            var accessToken = await _oauthClient.GetToken(code);

            return accessToken.AccessToken != null;
        }


        public async Task RefreshTokenAsync()
        {
        }

        public async Task<User> GetUserProfile()
        {
            var user = await _oauthClient.GetUserProfile();

            return await Task.FromResult(user);
        }
    }
}
