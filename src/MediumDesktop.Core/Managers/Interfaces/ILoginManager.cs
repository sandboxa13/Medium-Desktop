using System.Threading.Tasks;

namespace MediumDesktop.Core.Managers.Interfaces
{
    public interface ILoginManager
    {
        Task LoginAsync(string username, string password);
    }
}
