using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.Managers;
using Medium.Core.Managers.Interfaces;
using Medium.Core.MediumAPI;
using Medium.Core.Services;
using Medium.ViewModels;

namespace Medium.Views
{
    public class MainWindow : Window
    {
        private readonly IContainer _container = new Container().WithMefAttributedModel();

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            Task.Run(async () =>
            {
                AvaloniaXamlLoaderPortableXaml.Load(this);

                _container.RegisterExports(new[] { typeof(MainWindow).GetAssembly() });

                _container.RegisterExports(new[] { typeof(LoginManager).GetAssembly() });

                var configuration = _container.Resolve<IConfiguration>();
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                await configuration.AddJsonFile("appsettings.json");

                DataContext = new MainWindowViewModel(_container.Resolve<IApiController>(), _container.Resolve<INavigationService>());
            });
        }
    }
}
