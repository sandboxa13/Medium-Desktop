using Avalonia;
using Avalonia.Logging.Serilog;
using Medium.Views;

namespace Medium
{
    class Program
    {

        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();
    }
}
