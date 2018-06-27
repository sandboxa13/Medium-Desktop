using DryIoc;
using DryIoc.MefAttributedModel;
using MediumDesktop.Core.Extensions;
using MediumDesktop.Core.Managers.Interfaces;
using MediumDesktop.Core.Services;
using MediumDesktop.Core.ViewModels;

namespace MediumDesktop
{   
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App
    {
        private readonly IContainer _container = new Container();

        public App()
        {
            _container.RegisterExports(new[] { typeof(App).GetAssembly() });

            _container.RegisterShared();


            _container.Resolve<INavigationService>().Navigate<LoginViewModel>();
        }
    }
}
