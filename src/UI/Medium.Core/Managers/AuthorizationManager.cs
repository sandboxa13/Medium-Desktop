using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Services.Authentication;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]   
    public sealed class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAuthenticationService _AuthenticationService;

        public AuthenticationManager(IAuthenticationService AuthenticationService)
        {
            _AuthenticationService = AuthenticationService;
        }   

        public async Task LoginAsync()
        {
            await _AuthenticationService.AuthorizateAsync();
        }
    }
}
