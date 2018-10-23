using System.Threading.Tasks;
using Medium.Domain.Domain;

namespace Medium.Services.MediumApi
{
    public interface IMediumApiService
    {
        Task<User> GetUserProfile();
    }
}
