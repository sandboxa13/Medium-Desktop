using System;
using System.Threading.Tasks;

namespace Medium.Services.MediumApi.Interfaces
{   
    public interface IAuthenticationManager 
    {
        Task LoginAsync();

        IObservable<bool> LoggedIn();
    }
}
