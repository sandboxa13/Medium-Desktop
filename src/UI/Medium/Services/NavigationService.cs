using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using DryIocAttributes;
using Medium.Core.Services;
using Medium.Views;

namespace Medium.Services
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(INavigationService))]
    public class NavigationService : INavigationService 
    {
        private readonly Dictionary<string, Control> _navigationTargets;

        public NavigationService()
        {
            _navigationTargets = new Dictionary<string, Control>
            {
                {"LoginView", new LoginView()},
                {"MainPageView", new MainPageView()},
            };

        }

        public Task NavigateAsync<TViewModel>() where TViewModel : class
        {
            throw new NotImplementedException();
        }

        public IObservable<Type> Navigated { get; }
    }
}
