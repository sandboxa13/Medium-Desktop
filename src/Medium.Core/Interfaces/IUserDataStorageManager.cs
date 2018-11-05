using System.Threading.Tasks;

namespace Medium.Core.Interfaces
{   
    public interface IUserDataStorageManager<T> 
    {
        void InsertObject(T obj, string key);

        Task<T> GetObject(string key);
    }
}
