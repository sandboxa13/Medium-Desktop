using System;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Services;

namespace Medium.Services
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        public Task NavigateAsync<TViewModel>() where TViewModel : class
        {
            throw new NotImplementedException();
        }

        public IObservable<Type> Navigated { get; }
    }
}
