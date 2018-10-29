using System;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Services.Configuration;
using MediumSDK.Domain;

namespace Medium.Services.Authentication
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationService))]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfigurationService _configurationService;
        private readonly MediumClient _mediumClient;

        public AuthenticationService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;

            _mediumClient = new MediumClient(
                "ce250fa7c114",
                "bb152d21f43b20de5174495f488cd71aede8efaa",
                "text");
        }
                
        public async Task AuthorizateAsync()
        {
            await _mediumClient.AuthenticateUser();
        }

        public Task RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
