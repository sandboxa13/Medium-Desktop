using System.Threading.Tasks;

namespace Medium.Core.Managers
{   
    public interface IAuthenticationManager 
    {   
        Task<bool> LoginAsync();
    }
}
