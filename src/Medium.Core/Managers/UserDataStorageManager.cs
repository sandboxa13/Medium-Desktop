using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DryIocAttributes;
using Medium.Core.Interfaces;

namespace Medium.Core.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IUserDataStorageManager<>))]
    public class UserDataStorageManager<T> : IUserDataStorageManager<T>
    {
        public UserDataStorageManager()
        {
            BlobCache.ApplicationName = "MediumDesktop";
        }

        public void InsertObject(T obj, string key)
        {
            BlobCache.UserAccount.InsertObject(key, obj);
        }

        public async Task<T> GetObject(string key)
        {   
           return await BlobCache.UserAccount.GetObject<T>(key);
        }
    }
}
