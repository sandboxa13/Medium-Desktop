using System.Threading.Tasks;
using MediumSDK.WPF.Domain;

namespace MediumDesktop.Core.MediumAPI
{
    public interface IApiController
    {   
        Task<bool> AuthorizateAsync();

        Task RefreshTokenAsync();

        Task<User> GetUserProfile();
    }
}
