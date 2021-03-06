﻿using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DryIocAttributes;
using Medium.Services.MediumApi.Interfaces;

namespace Medium.Services.MediumApi.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IUserDataStorageManager<>))]
    public class UserDataStorageManager<T> : IUserDataStorageManager<T> where T : class, new()
    {
        public UserDataStorageManager()
        {
            BlobCache.ApplicationName = "MediumDesktop";
        }

        public void InsertObject(T obj, string key)
        {
            BlobCache.LocalMachine.InsertObject(key, obj);
        }

        public async Task<T> GetObject(string key)
        {
            return await BlobCache.LocalMachine.GetObject<T>(key)
                .Catch(Observable.Return(new T()));
        }
    }
}