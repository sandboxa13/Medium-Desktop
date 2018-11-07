using System;
using Akavache;
using DryIocAttributes;
using Medium.Services.MediumApi.Interfaces;

namespace Medium.Services.MediumApi.Managers
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
            BlobCache.LocalMachine.InsertObject(key, obj);
        }

        public IObservable<T> GetObject(string key)
        {   
           return BlobCache.LocalMachine.GetObject<T>(key);
        }
    }
}