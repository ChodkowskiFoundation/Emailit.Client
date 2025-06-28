using System.Text.Json.Serialization;

namespace Emailit.Client.Models
{
    public class EmailitDnsRecord
    {
        [JsonPropertyName("required")]
        public bool Required { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        [JsonPropertyName("ttl")]
        public string TTL { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
