using System.Threading.Tasks;
using Medium.Domain.Domain;

namespace Services.Interfaces.Interfaces
{
    public interface IMediumApiService
    {
        Task<User> GetUserProfile();
    }
}
