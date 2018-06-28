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

            var mainWindow = new MainWindow();

            if (mainWindow.Frame == null)
            {
                mainWindow.Frame = new Frame();
            }

            var frame = mainWindow.Frame;

            if (frame.Content == null)
            {
                _container.Resolve<INavigationService>().Navigate<LoginViewModel>();
            }

            mainWindow.Show();
        }
    }
}
