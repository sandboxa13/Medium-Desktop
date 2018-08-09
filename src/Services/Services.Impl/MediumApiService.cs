using System.Net;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Domain.Domain;
using Medium.Domain.Extensions;
using Medium.Domain.Routes;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IMediumApiService))]
    public class MediumApiService : IMediumApiService
    {
        private readonly IAuthorizationService _authorizationService;

        public MediumApiService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<User> GetUserProfile()
        {
            var tokenRequest = (HttpWebRequest)WebRequest.Create(MediumApiRoutes.UserProfile);
            tokenRequest.Method = "GET";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            tokenRequest.Headers["Authorization"] = "Bearer " + _authorizationService.GetToken().AccessToken;

            var user = tokenRequest.GetResponseJson<User>();

            return await Task.FromResult(user);
        }
    }
}
