using System;
using System.Reactive.Subjects;
using DryIocAttributes;
using Medium.Services.Navigation.Navigation;

namespace Medium.Services.Navigation
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        private readonly BehaviorSubject<PageIndex> _currentPageSubject;

        public NavigationService()
        {
            _currentPageSubject = new BehaviorSubject<PageIndex>(PageIndex.AuthorizationPage);
        }

        public void NavigateAsync(PageIndex pageIndex) => _currentPageSubject.OnNext(pageIndex);

        public IObservable<PageIndex> CurrentPage() => _currentPageSubject;
    }
}
