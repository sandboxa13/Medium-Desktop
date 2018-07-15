using System.Threading.Tasks;
using MediumDesktop.Core.Domain;

namespace MediumDesktop.Core.Managers.Interfaces
{
    public interface IStoreManager
    {
        Task<ApplicationData> GetApplicationData();
    }
}
