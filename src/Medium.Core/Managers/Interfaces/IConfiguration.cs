using System.Threading.Tasks;

namespace Medium.Core.Managers.Interfaces
{
    public interface IConfiguration
    {
        void SetBasePath(string basePath);

        Task AddJsonFile(string flieName);      
            
        string GetValue(string key);  
    }
}
