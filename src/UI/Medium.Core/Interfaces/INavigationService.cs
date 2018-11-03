using System;

namespace Medium.Core.Interfaces
{
    public interface INavigationService
    {
        void Navigate(Type viewModelType);

        IObservable<Type> CurrentPage();
    }
}