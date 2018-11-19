using System;
using System.Reactive;
using System.Reactive.Subjects;
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
        private readonly BehaviorSubject<bool> _loggedIn;

        public AuthenticationManager(
            IUserDataStorageManager<UserData> userDataStorageManager,
            IMediumClient mediumClient)
        {
            _userDataStorageManager = userDataStorageManager;
            _mediumClient = mediumClient;
            _loggedIn = new BehaviorSubject<bool>(false);
        }

        public IObservable<bool> LoggedIn() => _loggedIn;

        public async Task LoginAsync()
        {
            var userData = await _userDataStorageManager.GetObject("user");

            if (string.IsNullOrEmpty(userData.Token))
                await LoginHandler();
            else
            {
                UpdateClient(userData);
                _loggedIn.OnNext(true);
            }
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
            {
                _loggedIn.OnNext(false);
                throw new AuthenticationException("Token is Null");
            }

            var userData = new UserData
            {
                Token = _mediumClient.Token.AccessToken,
                RefreshToken = _mediumClient.Token.RefreshToken
            };

            _userDataStorageManager.InsertObject(userData, "user");

            _loggedIn.OnNext(true);
        }
    }
}