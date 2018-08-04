using Medium.Core.Domain;

namespace Medium.Core.Managers.Interfaces
{
    public interface IConfiguration
    {
        void SetBasePath(string basePath);
            
        void AddJsonFile(string path);  

        AppSettingsItem GetAppSettingsItem();
    }
}
