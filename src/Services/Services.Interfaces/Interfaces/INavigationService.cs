using System;
using Medium.Domain.Navigation;

namespace Services.Interfaces.Interfaces
{       
    public interface INavigationService
    {
        void NavigateAsync(PageIndex pageIndex);

        IObservable<PageIndex> CurrentPage();
    }
}
