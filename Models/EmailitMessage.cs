using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Emailit.Client.Models
{
    public class EmailitMessage
    {
        public EmailitMessage()
        {
            Attachments = new List<EmailitAttachment>();
        }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("reply_to")]
        public string ReplyTo { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("attachments")]
        public List<EmailitAttachment> Attachments { get; set; }

        [JsonIgnore]
        public bool HasAttachments => Attachments != null && Attachments.Count > 0;


    }
}