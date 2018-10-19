using System;
using System.IO;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Gtk3;
using Avalonia.Logging.Serilog;
using Avalonia.Platform;
using Avalonia.Threading;
using DryIoc;
using DryIoc.MefAttributedModel;
using ImTools;
using Medium.Core.Extensions;
using Medium.Core.ViewModels;
using Medium.Views;
using ReactiveUI;
using Services.Interfaces.Interfaces;

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

        private IDisposable Init()
        {
            return Disposable.Create(() => {});
        }

        public void RunApplication()
        {
            Application.Current.Do(application =>
            {
                RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

                var dataContext = _container.Resolve<MainWindowViewModel>();
                var app = new App(new App.Initializer(Init));

                var configuration = _container.Resolve<IConfigurationService>();
                var path = new DirectoryInfo(@"../..").FullName;
                configuration.SetBasePath(path);
                configuration.AddJsonFile("appsettings.json");

                var builder = AppBuilder.Configure(app)
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
            });
        }
    }
}
