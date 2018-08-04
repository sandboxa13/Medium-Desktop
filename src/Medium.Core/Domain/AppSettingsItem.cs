using System.Collections.Generic;
using Newtonsoft.Json;

namespace Medium.Core.Domain
{
    public class AppSettingsItem
    {
        [JsonProperty(PropertyName = "ClientId")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "ClientSecret")]
        public string ClientSecret { get; set; }
    }
}
