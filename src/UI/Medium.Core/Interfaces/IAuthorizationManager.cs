using System.Threading.Tasks;

namespace Medium.Core.Interfaces
{   
    public interface IAuthorizationManager
    {   
        Task<bool> LoginAsync();
    }
}
