using System.Threading.Tasks;
using MediumSDK.Net.Domain;

namespace Medium.Services.MediumApi.Interfaces
{
    public interface IUserProfileManager
    {
        Task<MediumUser> GetUser();
    }
}
