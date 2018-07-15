using System.Net;
using System.Threading.Tasks;
using DryIocAttributes;
using LiteDB;
using MediumDesktop.Core.Domain;
using MediumSDK.WPF.Domain;

namespace MediumDesktop.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        private readonly LiteDatabase _liteDatabase;
        private readonly string _redirectUrl = $"http://{IPAddress.Loopback}:{3000}/";
        private Token _accessToken;

        public ApiController(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public async Task<bool> AuthorizateAsync()
        {
            var result = await GetApplicationData();

            var oAuthClient = new OauthClient(result.ClientId, result.ClientSecret, _redirectUrl, "text");

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

        private async Task<ApplicationData> GetApplicationData()
        {
            return await Task.Run(() =>
            {
                var applicationDataCollection = _liteDatabase.GetCollection<ApplicationData>("application");

                var result = applicationDataCollection.FindOne(x => x.Id == 1);

                return result;
            });
        }
    }
}
