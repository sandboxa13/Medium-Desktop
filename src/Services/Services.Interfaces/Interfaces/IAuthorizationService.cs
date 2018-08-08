using System.Threading.Tasks;

namespace Services.Interfaces.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizateAsync();

        Task RefreshTokenAsync();
    }
}
