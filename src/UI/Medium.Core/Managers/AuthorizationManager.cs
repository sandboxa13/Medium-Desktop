using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Services.Authorization;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthorizationManager))]   
    public sealed class AuthorizationManager : IAuthorizationManager
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationManager(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }   

        public Task<bool> LoginAsync()
        {
            return _authorizationService.AuthorizateAsync();
        }
    }
}
