using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Medium.Core.MediumAPI;

namespace Medium.Core.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(ILoginManager))]
    public sealed class LoginManager : ILoginManager
    {
        private readonly IApiController _apiController;

        public LoginManager(IApiController apiController)
        {
            _apiController = apiController;
        }   

        public async Task<bool> LoginAsync()
        {
            return await _apiController.AuthorizateAsync();
        }
    }
}
