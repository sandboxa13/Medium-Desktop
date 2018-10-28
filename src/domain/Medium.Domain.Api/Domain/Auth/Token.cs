using System;
using Medium.Domain.Api.Converters;
using Medium.Domain.Api.Domain.Api;
using Newtonsoft.Json;

namespace Medium.Domain.Api.Domain.Auth
{
    public class Token
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        public Scope[] Scope { get; set; }

        [JsonProperty(PropertyName = "expires_at")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime ExpiresAt { get; set; }
    }
}
