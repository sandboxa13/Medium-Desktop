using System.Threading.Tasks;

namespace Medium.Core.Managers.Interfaces
{
    public interface ILoginManager
    {   
        Task<bool> LoginAsync();
    }
}
