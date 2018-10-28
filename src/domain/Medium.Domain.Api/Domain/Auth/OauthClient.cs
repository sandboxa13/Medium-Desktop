namespace Medium.Domain.Api.Domain.Auth
{
    public class OauthClient
    {
        public Token Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string State { get; set; }

        public OauthClient(string clientId, string clientSecret, string state)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            State = state;
        }
    }
}
