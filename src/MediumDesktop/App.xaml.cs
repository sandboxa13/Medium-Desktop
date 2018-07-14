using System.IO;
using System.Windows;
using System.Windows.Controls;
using DryIoc;
using DryIoc.MefAttributedModel;
using LiteDB;
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
            var localFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(localFolder, "MediumDesktop.db");
            _container.RegisterDelegate(x => new LiteDatabase(filePath), Reuse.Singleton);
            _container.RegisterExports(new[] { typeof(App).GetAssembly() });
            _container.RegisterShared();

            var mainWindow = new MainWindow();

            if (mainWindow.RootFrame == null)
            {
                mainWindow.RootFrame = new Frame();
            }

            var frame = mainWindow.RootFrame;

            if (frame.Content == null)
            {
                _container.Resolve<INavigationService>().NavigateAsync<LoginViewModel>();
            }

            mainWindow.Show();
        }
    }
}
