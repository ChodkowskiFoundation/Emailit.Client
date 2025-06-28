using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Emailit.Client.Models;
using Emailit.Client.Responses;

namespace Emailit.Client.Extensions
{
    public static class EmailitMessageExtensions
    {
        public static EmailitMessage AddAttachment(this EmailitMessage message, EmailitAttachment attachment)
        {
            if (attachment == null) throw new ArgumentNullException(nameof(attachment));

            if (message.Attachments == null) message.Attachments = new List<EmailitAttachment>() { attachment };
            else message.Attachments.Add(attachment);

            return message;
        }

        public static EmailitMessage AddAttachment(this EmailitMessage message, byte[] bytes, string fileName, string contentType = null)
        {
            var attachment = EmailitAttachment.FromByteArray(bytes, fileName, contentType);
            return message.AddAttachment(attachment);
        }

        public static EmailitMessage AddFrom(this EmailitMessage message, string emailAddress, string name = null)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));

            message.From = string.IsNullOrWhiteSpace(name)
                ? message.From = emailAddress
                : message.From = $"{name} {emailAddress}";

            return message;
        }

        public static Task<EmailitData<SendEmailResponse>> SendAsync(this EmailitMessage message, string apiKey) =>
            new EmailitClient(apiKey).SendEmailAsync(message);

        public static Task<EmailitData<SendEmailResponse>> SendAsync(this EmailitMessage message, EmailitConfiguration configuration) =>
            new EmailitClient(configuration).SendEmailAsync(message);

        public static Task<EmailitData<SendEmailResponse>> SendAsync(this EmailitMessage message, Action<EmailitConfiguration> configAction) =>
            new EmailitClient().Configure(configAction).SendEmailAsync(message);

        public static Task<EmailitData<SendEmailResponse>> SendAsync(this EmailitMessage message, EmailitClient emailitClient) =>
            emailitClient.SendEmailAsync(message);

        public static Task<EmailitData<SendEmailResponse>> SendAsync(this EmailitMessage message, Func<EmailitClient> emailitClientFactory) =>
            emailitClientFactory.Invoke().SendEmailAsync(message);
    }
}
