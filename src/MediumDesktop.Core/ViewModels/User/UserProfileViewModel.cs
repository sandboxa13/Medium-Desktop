using DryIocAttributes;

namespace MediumDesktop.Core.ViewModels.User
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfileViewModel))]
    public sealed class UserProfileViewModel : BaseViewModel
    {

    }
}
