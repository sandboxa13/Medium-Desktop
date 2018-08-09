using System.Threading.Tasks;
using Medium.Domain.Domain;

namespace Services.Interfaces.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizateAsync();

        Task RefreshTokenAsync();

        Token GetToken();
    }
}
