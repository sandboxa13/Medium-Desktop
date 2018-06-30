using System.Windows;
using DryIocAttributes;
using MediumDesktop.Core.Services;

namespace MediumDesktop.Services
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IMainWindowService))]
    public class MainWindowService : IMainWindowService
    {
        public void ActivateWindow()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Activate();
        }
    }
}
