using System.Security.Authentication;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Interfaces;
using MediumSDK.Net.Domain;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]   
    public sealed class AuthenticationManager : IAuthenticationManager
    {
        private readonly IMainWindowManager _mainWindowManager;
        private readonly MediumClient _mediumClient;
            
        public AuthenticationManager(IMainWindowManager mainWindowManager)
        {
            _mainWindowManager = mainWindowManager;
            _mediumClient = new MediumClient(
                "ce250fa7c114",
                "bb152d21f43b20de5174495f488cd71aede8efaa",
                "text");
        }   

        public async Task LoginAsync()
        {
            await _mediumClient.AuthenticateUser();
            var token = _mediumClient.Token.AccessToken;
            if (token == null) throw new AuthenticationException("Token is null");
            
            // TODO Persist token into local cache storage
            _mainWindowManager.Activate();
        }
    }
}
