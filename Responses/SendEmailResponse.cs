using System;
using System.Text.Json.Serialization;

using Emailit.Client.Models;

namespace Emailit.Client.Responses
{
    public class SendEmailResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("workspace_id")]
        public string WorkspaceId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("rcpt_to")]
        public string RcptTo { get; set; }

        [JsonPropertyName("mail_from")]
        public string MailFrom { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("sending_domain_id")]
        public int SendingDomainId { get; set; }

        [JsonPropertyName("credential_id")]
        public int CredentialId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("held")]
        public int Held { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("last_delivery_attempt")]
        public DateTime? LastDeliveryAttempt { get; set; }

        [JsonPropertyName("inspected")]
        public int Inspected { get; set; }

        [JsonPropertyName("spam")]
        public int Spam { get; set; }

        [JsonPropertyName("spam_score")]
        public string SpamScore { get; set; }

        [JsonPropertyName("threat")]
        public int Threat { get; set; }

        [JsonPropertyName("bounce")]
        public int Bounce { get; set; }

        [JsonPropertyName("received_with_ssl")]
        public int ReceivedWithSssl { get; set; }

        [JsonPropertyName("tracked_links")]
        public int TrackedLinks { get; set; }

        [JsonPropertyName("tracked_images")]
        public int TrackedImages { get; set; }

        [JsonPropertyName("parsed")]
        public int Parsed { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAtUtc { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAtUtc { get; set; }

        [JsonPropertyName("sending_domain")]
        public EmailitSendingDomain SendingDomain { get; set; }

        [JsonPropertyName("html_content")]
        public string HtmlContent { get; set; }

        [JsonPropertyName("text_content")]
        public string TextContent { get; set; }
    }
}
