using System.Text.Json.Serialization;

namespace Emailit.Client.Responses
{
    public class EmailitResult : IEmailitResult
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("notify")]
        public bool Notify { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
