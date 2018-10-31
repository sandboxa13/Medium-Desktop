using System.Threading.Tasks;
using Medium.Core.Domain;

namespace Medium.Core.Interfaces
{   
    public interface IAuthenticationManager 
    {   
        Task<AuthResult> LoginAsync();
    }
}
