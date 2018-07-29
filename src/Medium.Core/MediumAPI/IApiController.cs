using System.Threading.Tasks;
using Medium.SDK.Domain;

namespace Medium.Core.MediumAPI
{
    public interface IApiController
    {   
        Task<bool> AuthorizateAsync();

        Task RefreshTokenAsync();

        Task<User> GetUserProfile();
    }
}
