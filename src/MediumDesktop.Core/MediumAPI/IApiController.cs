using System.Threading.Tasks;

namespace MediumDesktop.Core.MediumAPI
{
    public interface IApiController
    {
        Task AuthorizateAsync();
    }
}
