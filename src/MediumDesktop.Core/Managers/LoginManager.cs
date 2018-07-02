using System.Threading.Tasks;
using DryIocAttributes;
using MediumDesktop.Core.Managers.Interfaces;
using MediumDesktop.Core.MediumAPI;

namespace MediumDesktop.Core.Managers
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
