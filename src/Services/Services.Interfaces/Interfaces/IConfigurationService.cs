using System.Threading.Tasks;

namespace Services.Interfaces.Interfaces
{
    public interface IConfigurationService
    {
        void SetBasePath(string basePath);

        Task AddJsonFile(string flieName);

        T GetValue<T>(string key);
    }
}
