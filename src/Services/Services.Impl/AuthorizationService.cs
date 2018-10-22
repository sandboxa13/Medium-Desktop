using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Domain.Domain;
using Medium.Domain.Routes;
using Newtonsoft.Json;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthorizationService))]
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfigurationService _configurationService;
        private OauthClient _oauthClient;

        public AuthorizationService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<bool> AuthorizateAsync()
        {
            _oauthClient = new OauthClient(
                _configurationService.GetValue<string>("ClientID"),
                _configurationService.GetValue<string>("ClientSecret"),
                "text");

            var code = await GetAuthCode();

            var accessToken = await GetToken(code);

            return accessToken.AccessToken != null;
        }

        public Task RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Token GetToken() => _oauthClient.Token;

        private async Task<string> GetAuthCode()
        {
            var http = new HttpListener();
            http.Prefixes.Add(MediumApiRoutes.RedirectUrl);
            http.Start();

            var url =
                $"{MediumApiRoutes.Authorize}?client_id={_oauthClient.ClientId}&scope=basicProfile,publishPost&state={_oauthClient.State}&response_type=code&redirect_uri={MediumApiRoutes.RedirectUrl}";

            var proc = new Process
            {
                StartInfo =
                {
                    UseShellExecute = true,
                    FileName = url
                }
            };
            proc.Start();

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

        private async Task<Token> GetToken(string code)
        {
            var tokenRequestBody =
                $"code={code}&client_id={_oauthClient.ClientId}&client_secret={_oauthClient.ClientSecret}&grant_type=authorization_code&redirect_uri={Uri.EscapeDataString(MediumApiRoutes.RedirectUrl)}";

            var tokenRequest = (HttpWebRequest)WebRequest.Create(MediumApiRoutes.Token);
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

                _oauthClient.Token = JsonConvert.DeserializeObject<Token>(responseText);
            }

            return await Task.FromResult(_oauthClient.Token);
        }
    }
}
