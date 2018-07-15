using DryIocAttributes;
using ReactiveUI;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfileViewModel))]
    public sealed class UserProfileViewModel : ReactiveObject
    {

    }
}
