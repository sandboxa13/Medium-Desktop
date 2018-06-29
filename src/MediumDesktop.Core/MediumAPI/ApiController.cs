using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DryIocAttributes;

namespace MediumDesktop.Core.MediumAPI
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IApiController))]
    public sealed class ApiController : IApiController
    {
        public async Task AuthorizateAsync()
        {
            var redirectURI = $"http://{IPAddress.Loopback}:{3000}/";
            var state = "text";
            var http = new HttpListener();
            http.Prefixes.Add(redirectURI);
            http.Start();

            var url = string.Format("{0}?client_id={1}&scope=basicProfile,publishPost&state={2}&response_type=code&redirect_uri={3}",
                "https://medium.com/m/oauth/authorize",
                "ce250fa7c114",
                state,
                System.Uri.EscapeDataString(redirectURI)
                );

            System.Diagnostics.Process.Start(url);
            var context = await http.GetContextAsync();
            var response = context.Response;
            var responseString = "<html><head><meta http-equiv=\'refresh\' content=\'10;url=https://google.com\'></head><body>Please return to the app.</body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
            });

            if (context.Request.QueryString.Get("error") != null)
            {
                return;
            }
            if (context.Request.QueryString.Get("code") == null
                || context.Request.QueryString.Get("state") == null)
            {
                return;
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");
            var incoming_state = context.Request.QueryString.Get("state");

            // Compares the receieved state to the expected value, to ensure that
            // this app made the request which resulted in authorization.
            if (incoming_state != state)
            {
                return;
            }

        }

        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

    }
}
