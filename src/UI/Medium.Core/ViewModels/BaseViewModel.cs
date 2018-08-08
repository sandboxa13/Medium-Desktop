using DryIocAttributes;
using ReactiveUI;

namespace Medium.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(BaseViewModel))]
    public class BaseViewModel : ReactiveObject
    {
    }
}
