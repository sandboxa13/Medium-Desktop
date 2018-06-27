using System;
using System.Threading.Tasks;

namespace MediumDesktop.Core.Services
{
    public interface INavigationService
    {
        Task Navigate<TViewModel>() where TViewModel : class;

        Task Navigate<TViewModel>(object parameter) where TViewModel : class;

        IObservable<Type> Navigated { get; }
    }
}
