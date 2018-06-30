using System.Threading.Tasks;

namespace MediumDesktop.Core.Managers.Interfaces
{
    public interface ILoginManager
    {   
        Task<bool> LoginAsync();
    }
}
