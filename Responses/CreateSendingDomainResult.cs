using System.Text.Json.Serialization;

using Emailit.Client.Models;

namespace Emailit.Client.Responses
{
    public class CreateSendingDomainResult : EmailitSendingDomain, IEmailitResult
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("notify")]
        public bool Notify { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
