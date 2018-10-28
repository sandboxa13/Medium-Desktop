using System.Threading.Tasks;
using Medium.Domain.Api.Domain;
using Medium.Domain.Api.Domain.Api;

namespace Medium.Services.MediumApi
{
    public interface IMediumApiService
    {
        Task<User> GetUserProfile(string token);
    }
}
