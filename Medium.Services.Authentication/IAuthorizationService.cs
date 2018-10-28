using System.Threading.Tasks;
using Medium.Domain.Api.Domain;
using Medium.Domain.Api.Domain.Auth;

namespace Medium.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizateAsync();

        Task RefreshTokenAsync();

        Token GetToken();
    }
}
