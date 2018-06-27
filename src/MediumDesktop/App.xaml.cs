using DryIoc;
using DryIoc.MefAttributedModel;
using MediumDesktop.Core.Extensions;

namespace MediumDesktop
{   
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App
    {
        private readonly IContainer _container = new Container().WithMefAttributedModel();

        public App()
        {
            InitializeComponent();

            _container.RegisterExports(new[] { typeof(App).GetAssembly() });

            _container.RegisterShared();
        }
    }
}
