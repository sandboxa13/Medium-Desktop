using System.Threading.Tasks;
using Medium.Domain.Domain;

namespace Medium.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizateAsync();

        Task RefreshTokenAsync();

        Token GetToken();
    }
}
