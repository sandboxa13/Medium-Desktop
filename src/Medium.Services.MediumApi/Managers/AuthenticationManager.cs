using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Services.MediumApi.Domain;
using Medium.Services.MediumApi.Interfaces;
using MediumSDK.Net.Domain;

namespace Medium.Services.MediumApi.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]
    public sealed class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserDataStorageManager<UserData> _userDataStorageManager;
        private readonly MediumClient _mediumClient;

        public AuthenticationManager(
            IUserDataStorageManager<UserData> userDataStorageManager)
        {
            _userDataStorageManager = userDataStorageManager;
            _mediumClient = new MediumClient(
                "ce250fa7c114",
                "bb152d21f43b20de5174495f488cd71aede8efaa",
                "text");
        }

        public Task LoginAsync()
        {
            _userDataStorageManager.GetObject("userData").Subscribe(
                data =>
                {
                },
                async exc =>
                {
                    await LoginHandler();
                });

            return Task.CompletedTask;
        }

        private async Task LoginHandler()
        {
            await _mediumClient.AuthenticateUser();

            var token = _mediumClient.Token;

            if(token == null)
                throw new AuthenticationException("Token is Null");

            var userData = new UserData
            {
                Token = _mediumClient.Token.AccessToken,
                RefreshToken = _mediumClient.Token.RefreshToken
            };

            _userDataStorageManager.InsertObject(userData, "userData");
        }
    }
}
