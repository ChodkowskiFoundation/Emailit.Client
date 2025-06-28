using System;
using System.Text.Json.Serialization;

using Emailit.Client.Enums;

namespace Emailit.Client.Models
{
    public class EmailitCredential
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public EmailitCredentialType Type { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("last_used_at")]
        public DateTime? LastUsedAtUtc { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAtUtc { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
