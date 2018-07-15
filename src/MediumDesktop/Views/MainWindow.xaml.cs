using System.IO;
using DryIoc;
using DryIoc.MefAttributedModel;
using LiteDB;
using MediumDesktop.Core.Extensions;
using MediumDesktop.Core.Services;
using MediumDesktop.Core.ViewModels;

namespace MediumDesktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IContainer _container = new Container();

        public MainWindow()
        {
            InitializeComponent();

            var localFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(localFolder, "MediumDesktop.db");
            _container.RegisterDelegate(x => new LiteDatabase(filePath), Reuse.Singleton);
            _container.RegisterExports(new[] { typeof(App).GetAssembly() });
            _container.RegisterShared();

            _container.Resolve<INavigationService>().NavigateAsync<LoginViewModel>();
        }
    }
}
