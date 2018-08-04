using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.Managers.Interfaces;

namespace Medium.Views
{
    public class MainWindow : Window
    {
        private readonly IContainer _container = new Container().WithMefAttributedModel();

        public MainWindow()
        {
            InitializeComponent();

            _container.RegisterExports(new[] { typeof(MainWindow).GetAssembly() });
#if DEBUG
            this.AttachDevTools();
#endif

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }
    }
}
