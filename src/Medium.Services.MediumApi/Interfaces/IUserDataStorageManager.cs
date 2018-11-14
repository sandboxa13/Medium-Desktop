using System.Threading.Tasks;

namespace Medium.Services.MediumApi.Interfaces
{       
    public interface IUserDataStorageManager<T> 
    {
        void InsertObject(T obj, string key);   

        Task<T> GetObject(string key);
    }
}
