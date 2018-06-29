using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium;
using Medium.Authentication;
using Newtonsoft.Json;

namespace MediumDesktop.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        private OAuthClient _client;
        private string _clientId = "ce250fa7c114";
        private string _clientSecret = "76880f31a925708f1f04bc522c0c88b3e395edcb";
        private readonly string _redirectUrl = $"http://{IPAddress.Loopback}:{3000}/";

        public async Task AuthorizateAsync()
        {
            _client = new OAuthClient(_clientId, _clientSecret);

            var code = await GetAuthCode();

            var accessToken = await GetToken(code);

        }

        private async Task<string> GetAuthCode()
        {
            var state = "text";
            var http = new HttpListener();
            http.Prefixes.Add(_redirectUrl);
            http.Start();

            var url =
                $"https://medium.com/m/oauth/authorize?client_id={_clientId}&scope=basicProfile,publishPost&state={state}&response_type=code&redirect_uri={System.Uri.EscapeDataString(_redirectUrl)}";

            System.Diagnostics.Process.Start(url);
            var context = await http.GetContextAsync();
            var response = context.Response;
            var responseString = "<html><head><meta http-equiv=\'refresh\' content=\'10;url=https://google.com\'></head><body>Please return to the app.</body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;

            await responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
            });

            if (context.Request.QueryString.Get("error") != null)
            {
            }
            if (context.Request.QueryString.Get("code") == null
                || context.Request.QueryString.Get("state") == null)
            {
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");

            return await Task.FromResult(code);
        }

        private async Task<Token> GetToken(string code)
        {
            var tokenRequestURI = "https://api.medium.com/v1/tokens";
            var tokenRequestBody =
                $"code={code}&client_id={_clientId}&client_secret={_clientSecret}&grant_type=authorization_code&redirect_uri={System.Uri.EscapeDataString(_redirectUrl)}";

            var tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            var _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;
            var stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            var tokenResponse = await tokenRequest.GetResponseAsync();

            Token token;

            using (var reader = new StreamReader(tokenResponse.GetResponseStream()))
            {
                var responseText = await reader.ReadToEndAsync();

                token = JsonConvert.DeserializeObject<Token>(responseText);
            }

            return await Task.FromResult(token);
        }
    }
}
