using System;
using System.Reactive.Subjects;
using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Core.ViewModels;

namespace Medium.Core.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(INavigationService))]
    public sealed class NavigationService : INavigationService
    {
        private readonly BehaviorSubject<Type> _currentPage;

        public NavigationService() => _currentPage = new BehaviorSubject<Type>(typeof(LoginViewModel));

        public void Navigate(Type viewModel) => _currentPage.OnNext(viewModel);

        public IObservable<Type> CurrentPage() => _currentPage;
    }
}