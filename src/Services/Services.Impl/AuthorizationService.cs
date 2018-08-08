using System;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Domain.Domain;
using Medium.Domain.OAuth;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthorizationService))]
    public class AuthorizationService : IAuthorizationService   
    {
        private readonly IConfigurationService _configurationService;
        private OauthClient _oauthClient;
            
        public AuthorizationService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<bool> AuthorizateAsync()
        {
            _oauthClient = new OauthClient(
                _configurationService.GetValue<string>("ClientID"),
                _configurationService.GetValue<string>("ClientSecret"),
                "text");

            var code = await _oauthClient.GetAuthCode();

            var accessToken = await _oauthClient.GetToken(code);

            return accessToken.AccessToken != null;
        }

        public Task RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Token GetToken() => _oauthClient.Token;
    }
}
