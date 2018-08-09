using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Services.Interfaces.Interfaces;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(ILoginManager))]
    public sealed class LoginManager : ILoginManager
    {
        private readonly IAuthorizationService _authorizationService;

        public LoginManager(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }   

        public async Task<bool> LoginAsync()
        {
            return await _authorizationService.AuthorizateAsync();
        }
    }
}
