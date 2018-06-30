using System.Net;
using System.Threading.Tasks;
using DryIocAttributes;
using MediumSDK.WPF.Domain;

namespace MediumDesktop.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        private string _clientId = "ce250fa7c114";
        private string _clientSecret = "76880f31a925708f1f04bc522c0c88b3e395edcb";
        private readonly string _redirectUrl = $"http://{IPAddress.Loopback}:{3000}/";
        private Token _accessToken;

        public async Task<bool> AuthorizateAsync()
        {
            var oAuthClient = new OauthClient(_clientId, _clientSecret, _redirectUrl, "text");

            var code = await oAuthClient.GetAuthCode();

            var accessToken = await oAuthClient.GetToken(code);
            _accessToken = accessToken;

            return accessToken.AccessToken != null;
        }

        public async Task RefreshTokenAsync()
        {
        }
    }
}
