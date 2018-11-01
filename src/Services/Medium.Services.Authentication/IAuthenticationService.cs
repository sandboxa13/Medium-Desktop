using System.Threading.Tasks;

namespace Medium.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthorizateAsync();  

        Task RefreshTokenAsync();
    }
}
