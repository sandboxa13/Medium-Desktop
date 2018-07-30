using DryIocAttributes;
using ReactiveUI;

namespace Medium.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(BaseViewModel))]
    public class BaseViewModel : ReactiveObject
    {
    }
}
