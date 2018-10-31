using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Domain;
using Medium.Core.Interfaces;
using MediumSDK.Net.Domain;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]   
    public sealed class AuthenticationManager : IAuthenticationManager
    {   
        private readonly MediumClient _mediumClient;
            
        public AuthenticationManager()
        {
            _mediumClient = new MediumClient(
                "ce250fa7c114",
                "bb152d21f43b20de5174495f488cd71aede8efaa",
                "text");
        }   

        public async Task<AuthResult> LoginAsync()
        {
            await _mediumClient.AuthenticateUser();

            return !string.IsNullOrEmpty(_mediumClient.Token.AccessToken) ? 
                AuthResult.Succses : 
                AuthResult.Error;
        }
    }
}
