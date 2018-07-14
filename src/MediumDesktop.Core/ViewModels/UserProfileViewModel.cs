using DryIocAttributes;

namespace MediumDesktop.Core.ViewModels
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfileViewModel))]
    public sealed class UserProfileViewModel
    {

    }
}
