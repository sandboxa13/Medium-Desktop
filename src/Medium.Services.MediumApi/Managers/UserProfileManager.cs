using System;
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
        private readonly IAuthenticationManager _authenticationManager;

        public UserProfileManager(
            IMediumClient mediumClient, 
            IAuthenticationManager authenticationManager)
        {
            _mediumClient = mediumClient;
            _authenticationManager = authenticationManager;

            _authenticationManager.LoggedIn()
                .Subscribe(async b =>
            {
                var profile =  await GetUser();
            });
        }

        public Task<MediumUser> GetUser()
        {
            return _mediumClient.GetUser();
        }
    }
}
