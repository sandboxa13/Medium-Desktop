using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Medium.SDK.Domain;

namespace Medium.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        private readonly IConfigurationManager _configuration;
        private OauthClient _oauthClient;

        public ApiController(IConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AuthorizateAsync()  
        {
            _oauthClient = new OauthClient(
                _configuration.GetValue<string>("ClientID"),
                _configuration.GetValue<string>("ClientSecret"),
                "text");

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
