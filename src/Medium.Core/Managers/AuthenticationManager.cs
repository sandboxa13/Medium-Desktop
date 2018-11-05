using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Domain;
using Medium.Core.Interfaces;
using MediumSDK.Net.Domain;

namespace Medium.Core.Managers
{   
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthenticationManager))]   
    public sealed class AuthenticationManager : IAuthenticationManager
    {
        private readonly IMainWindowManager _mainWindowManager;
        private readonly IUserDataStorageManager<UserData> _userDataStorageManager;
        private readonly MediumClient _mediumClient;
            
        public AuthenticationManager(
            IMainWindowManager mainWindowManager, 
            IUserDataStorageManager<UserData> userDataStorageManager)
        {
            _mainWindowManager = mainWindowManager;
            _userDataStorageManager = userDataStorageManager;
            _mediumClient = new MediumClient(
                "ce250fa7c114",
                "bb152d21f43b20de5174495f488cd71aede8efaa",
                "text");
        }   

        public async Task LoginAsync()
        {
            try
            {
                var data = await _userDataStorageManager.GetObject("userData");

                //TODO insert this data to medium client 
            }
            catch (Exception)
            {
                await _mediumClient.AuthenticateUser();
                var token = _mediumClient.Token;
                if (token.AccessToken == null) throw new AuthenticationException("Token is null");

                var userData = new UserData
                {
                    Token = token.AccessToken,
                    RefreshToken = token.RefreshToken
                };

                _userDataStorageManager.InsertObject(userData, "userData");

                _mainWindowManager.Activate();
            }
        }
    }
}
