using System;
using Medium.Domain.Navigation;

namespace Medium.Services.Navigation
{       
    public interface INavigationService
    {
        void NavigateAsync(PageIndex pageIndex);

        IObservable<PageIndex> CurrentPage();
    }
}
