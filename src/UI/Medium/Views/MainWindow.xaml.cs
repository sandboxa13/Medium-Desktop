using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.Extensions;
using Medium.Core.Services;
using Medium.Core.ViewModels;
using Services.Interfaces.Interfaces;

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
            AvaloniaXamlLoaderPortableXaml.Load(this);

            _container.RegisterExports(new[] { typeof(MainWindow).GetAssembly() });
            _container.RegisterShared();

            var configuration = _container.Resolve<IConfigurationService>();
            configuration.SetBasePath(Directory.GetCurrentDirectory());
            configuration.AddJsonFile("appsettings.json");

            DataContext = new MainWindowViewModel(
                _container.Resolve<IMediumApiService>(),
                _container.Resolve<INavigationService>());
        }
    }
}
