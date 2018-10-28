using System.Net;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Domain.Api.Domain;
using Medium.Domain.Api.Domain.Api;
using Medium.Domain.Api.Extensions;
using Medium.Domain.Api.Routes;

namespace Medium.Services.MediumApi
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IMediumApiService))]
    public class MediumApiService : IMediumApiService
    {
        public async Task<User> GetUserProfile(string token)
        {
            var tokenRequest = (HttpWebRequest)WebRequest.Create(MediumApiRoutes.UserProfile);
            tokenRequest.Method = "GET";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            tokenRequest.Headers["Authorization"] = "Bearer " + /*_authorizationService.GetToken().AccessToken*/ token;

            var user = tokenRequest.GetResponseJson<User>();

            return await Task.FromResult(user);
        }
    }
}
