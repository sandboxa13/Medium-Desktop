﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Domain.Domain;
using Medium.Domain.OAuth;
using Newtonsoft.Json;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IAuthorizationService))]
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _redirectUrl = $"http://{IPAddress.Loopback}:{3000}/";
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
            http.Prefixes.Add(_redirectUrl);
            http.Start();

            var url =
                $"https://medium.com/m/oauth/authorize?client_id={_oauthClient.ClientId}&scope=basicProfile,publishPost&state={_oauthClient.State}&response_type=code&redirect_uri={Uri.EscapeDataString(_redirectUrl)}";

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

        private async Task<Token> GetToken(string code)
        {
            var tokenRequestURI = "https://api.medium.com/v1/tokens";
            var tokenRequestBody =
                $"code={code}&client_id={_oauthClient.ClientId}&client_secret={_oauthClient.ClientSecret}&grant_type=authorization_code&redirect_uri={Uri.EscapeDataString(_redirectUrl)}";

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

                _oauthClient.Token = JsonConvert.DeserializeObject<Token>(responseText);
            }

            return await Task.FromResult(_oauthClient.Token);
        }
    }
}
