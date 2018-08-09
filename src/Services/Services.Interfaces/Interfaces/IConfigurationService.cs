using System.Threading.Tasks;

namespace Services.Interfaces.Interfaces
{
    public interface IConfigurationService
    {
        string BasePath { get; set; }

        void SetBasePath(string basePath);

        Task AddJsonFile(string flieName);

        T GetValue<T>(string key);
    }
}
