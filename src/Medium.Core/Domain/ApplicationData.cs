using Newtonsoft.Json;

namespace Medium.Core.Domain
{   
    public class ApplicationData
    {
        public int Id { get; set; }

        public string ClientId { get; set; }


        [JsonProperty(PropertyName = "ClientSecret")]
        public string ClientSecret { get; set; }
    }
}
