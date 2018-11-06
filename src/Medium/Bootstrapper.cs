using Avalonia;
using Avalonia.Gtk3;
using Avalonia.Logging.Serilog;
using Avalonia.Platform;
using Avalonia.Threading;
using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.Extensions;
using Medium.Core.ViewModels;
using Medium.Views;
using ReactiveUI;

namespace Medium
{
    public class Bootstrapper
    {
        private readonly IContainer _container = new Container().WithMefAttributedModel();

        public Bootstrapper()
        {
            _container.RegisterExports(new[] { typeof(MainWindow).GetAssembly() });
            _container.RegisterShared();
        }

        public void RunApplication()
        {
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            var dataContext = _container.Resolve<MainWindowViewModel>();

            var builder = AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();

            var os = builder.RuntimePlatform.GetRuntimeInfo().OperatingSystem;

            if (os == OperatingSystemType.OSX)
            {
                builder.UseAvaloniaNative(null, opt =>
                {
                    opt.MacOptions.ShowInDock = true;
                    opt.UseDeferredRendering = true;
                    opt.UseGpu = true;
                }).UseSkia();
            }
            else if (os == OperatingSystemType.Linux)
            {
                builder.UseGtk3(new Gtk3PlatformOptions
                {
                    UseDeferredRendering = true,
                    UseGpuAcceleration = true
                }).UseSkia();
            }
            else
            {
                builder.UsePlatformDetect();
            }

            builder.UseReactiveUI();
            builder.Start<MainWindow>(() => dataContext);
        }
    }
}
