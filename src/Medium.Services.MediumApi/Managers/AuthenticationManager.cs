using System.Security.Authentication;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Services.MediumApi.Domain;
using Medium.Services.MediumApi.Interfaces;
using MediumSDK.Net;

namespace Medium.Services.MediumApi.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]
    public sealed class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserDataStorageManager<UserData> _userDataStorageManager;
        private readonly IMediumClient _mediumClient;

        public AuthenticationManager(
            IUserDataStorageManager<UserData> userDataStorageManager,
            IMediumClient mediumClient)
        {
            _userDataStorageManager = userDataStorageManager;
            _mediumClient = mediumClient;
        }

        public async Task LoginAsync()
        {
            var userData = await _userDataStorageManager.GetObject("userData");

            if (userData == null)
                await LoginHandler();
            else
                UpdateClient(userData);
        }

        private void UpdateClient(UserData userData)
        {
            _mediumClient.Token.AccessToken = userData.Token;
            _mediumClient.Token.RefreshToken = userData.RefreshToken;
        }

        private async Task LoginHandler()
        {
            await _mediumClient.AuthenticateUser();

            var token = _mediumClient.Token;

            if (token == null)
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
