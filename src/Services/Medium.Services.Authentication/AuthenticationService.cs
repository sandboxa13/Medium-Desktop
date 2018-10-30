using System;
using System.Threading.Tasks;
using DryIocAttributes;
using MediumSDK.Net.Domain;

namespace Medium.Services.Authentication
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationService))]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly MediumClient _mediumClient;

        public AuthenticationService()
        {
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
