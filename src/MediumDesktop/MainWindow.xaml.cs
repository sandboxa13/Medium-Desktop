using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace MediumDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            var clientId = "ce250fa7c114";
            var clientSecret = "76880f31a925708f1f04bc522c0c88b3e395edcb";


            var client = new Medium.OAuthClient(clientId, clientSecret);

            var url = client.GetAuthorizeUrl(
                "secretstate",
                "https://yoursite.com/callback/medium",
                new[]
                {
                    Medium.Authentication.Scope.BasicProfile
                });

            var req = WebRequest.Create(url);

            var resp = req.GetResponse();

            client.GetAccessToken("","https://yoursite.com/callback/medium");


           

            InitializeComponent();
        }
    }
}
