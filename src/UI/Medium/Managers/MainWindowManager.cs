using DryIocAttributes;
using Medium.Core.Interfaces;
using Medium.Core.Managers;

namespace Medium.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IMainWindowManager))]
    public class MainWindowManager : IMainWindowManager
    {
        public void Activate()
        {   
            var mainWIndow = App.Current.MainWindow;

            mainWIndow.Activate();
        }
    }
}
