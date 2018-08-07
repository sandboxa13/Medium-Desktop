using Avalonia;
using Avalonia.Markup.Xaml;

namespace Medium
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }
    }
}
