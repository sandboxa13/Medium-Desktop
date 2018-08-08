using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Medium.Domain.Domain;
using Medium.Domain.Extensions;
using Newtonsoft.Json;

namespace Medium.Domain.OAuth
{
    public class OauthClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _state;
        private readonly string _redirectUrl = $"http://{IPAddress.Loopback}:{3000}/";
        public Token Token { get; set; }
        public OauthClient(string clientId, string clientSecret, string state)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _state = state;
        }

        public async Task<string> GetAuthCode()
        {
            var http = new HttpListener();
            http.Prefixes.Add(_redirectUrl);
            http.Start();

            var url =
                $"https://medium.com/m/oauth/authorize?client_id={_clientId}&scope=basicProfile,publishPost&state={_state}&response_type=code&redirect_uri={Uri.EscapeDataString(_redirectUrl)}";

            System.Diagnostics.Process.Start(url);
            var context = await http.GetContextAsync();
            var response = context.Response;
            var responseString = "<html><head><meta http-equiv=\'refresh\' content=\'10;url=https://google.com\'></head><body>Please return to the app.</body></html>";
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;

            await responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
            });

            var code = context.Request.QueryString.Get("code");

            return await Task.FromResult(code);
        }

        public async Task<Token> GetToken(string code)
        {
            var tokenRequestURI = "https://api.medium.com/v1/tokens";
            var tokenRequestBody =
                $"code={code}&client_id={_clientId}&client_secret={_clientSecret}&grant_type=authorization_code&redirect_uri={Uri.EscapeDataString(_redirectUrl)}";

            var tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            var byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = byteVersion.Length;
            var stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
            stream.Close();

            var tokenResponse = await tokenRequest.GetResponseAsync();


            using (var reader = new StreamReader(tokenResponse.GetResponseStream() ?? throw new NullReferenceException()))
            {
                var responseText = await reader.ReadToEndAsync();

                _token = JsonConvert.DeserializeObject<Token>(responseText);
            }

            return await Task.FromResult(_token);
        }
            
        public async Task<User> GetUserProfile()
        {   
            var userProfileRequestUri = "https://api.medium.com/v1/me"; 

            var tokenRequest = (HttpWebRequest)WebRequest.Create(userProfileRequestUri);
            tokenRequest.Method = "GET";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            tokenRequest.Headers["Authorization"] = "Bearer " + _token.AccessToken;

            var user =  tokenRequest.GetResponseJson<User>();

            return await Task.FromResult(user);
        }
    }
}
