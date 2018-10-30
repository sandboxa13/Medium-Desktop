using System;
using Medium.Services.Navigation.Navigation;

namespace Medium.Services.Navigation
{       
    public interface INavigationService
    {
        void NavigateAsync(PageIndex pageIndex);

        IObservable<PageIndex> CurrentPage();
    }
}
