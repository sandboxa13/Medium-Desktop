using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Services.Authentication;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]   
    public sealed class AuthenticationManager : IAuthenticationManager
    {   
        private readonly IAuthenticationService _authenticationService; 

        public AuthenticationManager(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }   

        public async Task<bool> LoginAsync()
        {
            await _authenticationService.AuthorizateAsync();

            return true;
        }
    }
}
