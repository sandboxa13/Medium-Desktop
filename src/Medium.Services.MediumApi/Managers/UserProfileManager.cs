using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Services.MediumApi.Interfaces;
using MediumSDK.Net;
using MediumSDK.Net.Domain;

namespace Medium.Services.MediumApi.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IUserProfileManager))]
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IMediumClient _mediumClient;

        public UserProfileManager(
            IMediumClient mediumClient)
        {
            _mediumClient = mediumClient;
        }

        public Task<MediumUser> GetUser()
        {
            return _mediumClient.GetUser();
        }
    }
}
