using DryIocAttributes;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(MainPageViewModel))]
    public sealed class MainPageViewModel 
    {
    }
}
