using System.Threading.Tasks;

namespace Medium.Core.Managers.Interfaces
{   
    public interface IConfigurationManager
    {   
        void SetBasePath(string basePath);      
                
        Task AddJsonFile(string flieName);

        T GetValue<T>(string key);
    }
}
