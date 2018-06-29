using System.Threading.Tasks;
using DryIocAttributes;
using MediumDesktop.Core.Managers.Interfaces;

namespace MediumDesktop.Core.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(ILoginManager))]
    public sealed class LoginManager : ILoginManager
    {
        public async Task LoginAsync(string username, string password)
        {
            await Task.CompletedTask;
        }
    }
}
