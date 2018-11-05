using System.Threading.Tasks;

namespace Medium.Services.Configuration
{
    public interface IConfigurationService
    {
        string BasePath { get; set; }

        void SetBasePath(string basePath);

        Task AddJsonFile(string flieName);

        T GetValue<T>(string key);
    }
}
