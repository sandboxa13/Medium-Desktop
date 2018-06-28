using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DryIoc;
using DryIocAttributes;
using MediumDesktop.Core.Managers.Interfaces;
using MediumDesktop.Core.Services;
using MediumDesktop.Core.ViewModels;
using MediumDesktop.Views;

namespace MediumDesktop.Services
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(INavigationService))]
    public sealed class NavigationService : INavigationService
    {
        private readonly IResolver _resolver;

        private readonly Subject<Type> _navigatedSubject = new Subject<Type>();

        private readonly IReadOnlyDictionary<Type, Type> _pages = new Dictionary<Type, Type>
        {
            {typeof(LoginViewModel), typeof(LoginView)},
            {typeof(MainPageViewModel), typeof(MainPageView)},
        };

        public NavigationService(IResolver resolver)
        {
            _resolver = resolver;
        }

        public IObservable<Type> Navigated => _navigatedSubject;

        public Task Navigate<T>() where T : class => Navigate<T>(_resolver.Resolve<Func<T>>()());

        public async Task Navigate<T>(object parameter) where T : class
        {
            switch (typeof(T).Name)
            {
                case nameof(LoginViewModel):

                    ((Frame)Application.Current.MainWindow.Content).Navigate(new LoginView
                    {
                        DataContext = new LoginViewModel(_resolver.Resolve<ILoginManager>(), _resolver.Resolve<INavigationService>())
                    });

                    break;

                case nameof(MainPageViewModel):

                    ((Frame)Application.Current.MainWindow.Content).Navigate(new MainPageView
                    {
                        DataContext = new MainPageViewModel()
                    });
                    break;
            }
            //void NavigateFrame(Frame frame)
            //{
            //    var viewModelType = typeof(T);


            //    RaiseNavigated(viewModelType);
            //}
        }

        private void RaiseNavigated(Type type)
        {
            _navigatedSubject.OnNext(type);
        }

        private static T GetChild<T>(DependencyObject root, int depth) where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(root);
            for (var x = 0; x < childrenCount; x++)
            {
                var child = VisualTreeHelper.GetChild(root, x);
                if (child is T ths && depth-- == 0) return ths;
                var frame = GetChild<T>(child, depth);
                if (frame != null) return frame;
            }
            return null;
        }
    }
}
