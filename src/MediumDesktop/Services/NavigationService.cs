using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DryIoc;
using DryIocAttributes;
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
                    NavigateFrame(GetChild<Frame>(Application.Current.MainWindow, 0));
                    await Task.Delay(150);
                    break;
            }
            void NavigateFrame(Frame frame)
            {
                var viewModelType = typeof(T);
                if ((Page)frame.Content != null &&
                   ((Page)frame.Content).DataContext.GetType() == viewModelType &&
                frame.Navigate(_pages[viewModelType], parameter))
                ((Page)frame.Content).DataContext = parameter;
                RaiseNavigated(viewModelType);
            }
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
