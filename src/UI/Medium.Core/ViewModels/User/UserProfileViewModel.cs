using DryIocAttributes;

namespace Medium.Core.ViewModels.User
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfileViewModel))]
    public sealed class UserProfileViewModel : BaseViewModel
    {

    }
}
