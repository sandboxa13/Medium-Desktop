using System.Windows;
using System.Windows.Controls;
using DryIoc;
using DryIoc.MefAttributedModel;
using MediumDesktop.Core.Extensions;
using MediumDesktop.Core.Services;
using MediumDesktop.Core.ViewModels;
using MediumDesktop.Views;

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

            if (Current.MainWindow == null)
            {
                Current.MainWindow = new Window {Content = new Frame {Content = new Frame()}};
            }

            var frame = (Frame)Current.MainWindow.Content;

            if (frame.Content == null)
            {
                frame.Content = new LoginView();
                _container.Resolve<INavigationService>().Navigate<LoginViewModel>();
            }

            Current.MainWindow.Activate();
            Current.MainWindow.Show();
        }
    }
}
