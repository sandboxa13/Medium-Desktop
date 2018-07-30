using DryIocAttributes;

namespace Medium.ViewModels.User
{
    [Reuse(ReuseType.Transient)]
    [ExportEx(typeof(UserProfileViewModel))]
    public sealed class UserProfileViewModel : BaseViewModel
    {

    }
}
