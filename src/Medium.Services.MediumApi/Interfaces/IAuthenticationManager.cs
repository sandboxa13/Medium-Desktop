using System;
using System.Reactive;  
using System.Threading.Tasks;

namespace Medium.Services.MediumApi.Interfaces
{   
    public interface IAuthenticationManager 
    {
        Task LoginAsync();

        IObservable<Unit> LoggedIn();
    }
}
