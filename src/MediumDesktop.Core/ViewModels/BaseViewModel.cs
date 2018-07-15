using DryIocAttributes;
using ReactiveUI;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(BaseViewModel))]
    public class BaseViewModel : ReactiveObject
    {
    }
}
