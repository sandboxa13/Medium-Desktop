using System.Threading.Tasks;

namespace Medium.Services.Authentication
{
    public interface IAuthenticationService
    {   
        Task AuthorizateAsync();

        Task RefreshTokenAsync();
    }
}
