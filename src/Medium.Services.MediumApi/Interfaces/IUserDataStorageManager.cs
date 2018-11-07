using System;

namespace Medium.Services.MediumApi.Interfaces
{   
    public interface IUserDataStorageManager<T> 
    {
        void InsertObject(T obj, string key);

        IObservable<T> GetObject(string key);
    }
}
