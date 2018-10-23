using System.Threading.Tasks;

namespace Medium.Core.Managers.Interfaces
{   
    public interface IAuthorizationManager
    {   
        Task<bool> LoginAsync();
    }
}
