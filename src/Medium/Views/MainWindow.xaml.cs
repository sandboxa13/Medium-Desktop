using System.IO;
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
            _container.RegisterExports(new[] { typeof(MainWindow).GetAssembly() });

            _container.RegisterExports(new[] { typeof(LoginManager).GetAssembly() });
#if DEBUG
            this.AttachDevTools();
#endif


            var configuration = _container.Resolve<IConfiguration>();
            configuration.SetBasePath(Directory.GetCurrentDirectory());
            configuration.AddJsonFile("appsettings.json");

            var apiCOntroller = _container.Resolve<IApiController>();

            apiCOntroller.AuthorizateAsync();

            DataContext = new MainWindowViewModel(_container.Resolve<IApiController>(), _container.Resolve<INavigationService>());

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }
    }
}
