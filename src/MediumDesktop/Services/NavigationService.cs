using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DryIoc;
using DryIocAttributes;
using MediumDesktop.Core.Services;
using MediumDesktop.Core.ViewModels;
using MediumDesktop.Views;

namespace MediumDesktop.Services
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(INavigationService))]
    public sealed class WpfNavigationService : INavigationService
    {
        private readonly IResolver _resolver;
        private readonly Subject<Type> _navigatedSubject = new Subject<Type>();
        private NavigationService _navigationService;

        private readonly IReadOnlyDictionary<Type, Type> _pages = new Dictionary<Type, Type>
        {
            {typeof(LoginViewModel), typeof(LoginView)},
            {typeof(MainPageViewModel), typeof(MainPageView)},
        };

        public WpfNavigationService(IResolver resolver)
        {
            _resolver = resolver;
        }

        public IObservable<Type> Navigated => _navigatedSubject;

        public async Task NavigateAsync<T>() where T : class
        {
            var instanceView = (Page)Activator.CreateInstance(_pages[typeof(T)]);
            instanceView.DataContext = _resolver.Resolve(typeof(T));

            var navigationService = _navigationService ?? await GetNavigationService();
            navigationService.Navigate(instanceView);
        }


        private async Task<NavigationService> GetNavigationService()
        {
            var window = Application.Current.MainWindow;
            var frame = (Frame)window.FindName("RootFrame");
            _navigationService = frame.NavigationService;
            _navigationService.Navigated += (sender, args) =>
            {
                var viewType = args.Content.GetType();
                var viewModelType = _pages.FirstOrDefault(x => x.Value == viewType).Key;
            };
            return await Task.FromResult(_navigationService);
        }
    }
}
