using System;

using Emailit.Client.Extensions;
using Emailit.Client.Models;

namespace Emailit.Client
{
    public class EmailitMessageBuilder
    {
        protected readonly EmailitMessage _message;

        public EmailitMessageBuilder()
        {
            _message = new EmailitMessage();
        }

        public EmailitMessageBuilder(EmailitMessage message)
        {
            if (message != null) _message = message;
            else _message = new EmailitMessage();
        }

        /// <summary>
        /// Gets the current state of <see cref="EmailitMessage"/> (no validation provided).
        /// </summary>
        /// <returns><see cref="EmailitMessage"/> in it's current state (valid or not).</returns>
        public EmailitMessage Message => _message;

        /// <summary>
        /// Adds <see cref="EmailitAttachment"/> to <see cref="EmailitMessage"/> internally.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="attachment"/> is null.</exception>
        public EmailitMessageBuilder AddAttachment(EmailitAttachment attachment)
        {
            _message.AddAttachment(attachment);

            return this;
        }

        /// <summary>
        /// Creates and adds <see cref="EmailitAttachment"/> to <see cref="EmailitMessage"/> internally.
        /// </summary>
        /// <param name="bytes">Attachment file as byte array.</param>
        /// <param name="fileName">File name with file extension.</param>
        /// <param name="contentType">File MIME content type (assumed from <paramref name="fileName"/> if not provided).</param>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="bytes"/> or <paramref name="fileName"/> is null.</exception>
        /// <exception cref="ArgumentException">When <paramref name="contentType"/> is null, empty or whitespace and it cannot be assumed from <paramref name="fileName"/>.</exception>
        public EmailitMessageBuilder AddAttachment(byte[] bytes, string fileName, string contentType = null)
        {
            _message.AddAttachment(bytes, fileName, contentType);

            return this;
        }

        /// <summary>
        /// Supplies <see cref="EmailitMessage.From"/> property internally. 
        /// Output format is "<paramref name="emailAddress"/>" or "<paramref name="name"/> <![CDATA[<]]><paramref name="emailAddress"/><![CDATA[>]]> if <paramref name="name"/> is provided."
        /// </summary>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="emailAddress"/> is null, empty or whitespace.</exception>
        public EmailitMessageBuilder From(string emailAddress, string name = null)
        {
            _message.AddFrom(emailAddress, name);
            
            return this;
        }

        /// <summary>
        /// Supplies <see cref="EmailitMessage.ReplyTo"/> property internally. 
        /// </summary>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="emailAddress"/> is null, empty or whitespace.</exception>
        public EmailitMessageBuilder ReplyTo(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));

            _message.ReplyTo = emailAddress;

            return this;
        }

        /// <summary>
        /// Supplies <see cref="EmailitMessage.Subject"/> property internally. 
        /// </summary>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="subject"/> is null, empty or whitespace.</exception>
        public EmailitMessageBuilder Subject(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException(nameof(subject));

            _message.Subject = subject;

            return this;
        }

        /// <summary>
        /// Supplies <see cref="EmailitMessage.To"/> property internally. 
        /// </summary>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="emailAddress"/> is null, empty or whitespace.</exception>
        public EmailitMessageBuilder To(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));

            _message.To = emailAddress;

            return this;
        }

        /// <summary>
        /// Supplies <see cref="EmailitMessage.Html"/> property internally. 
        /// </summary>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="htmlContent"/> is null, empty or whitespace.</exception>
        /// <exception cref="InvalidOperationException">When <see cref="EmailitMessage"/> already has <see cref="EmailitMessage.Text"/> supplied.</exception>
        public EmailitMessageBuilder WithHtmlContent(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
                throw new ArgumentNullException(nameof(htmlContent));
            if (!string.IsNullOrWhiteSpace(_message.Html))
                throw new InvalidOperationException("Emailit Message already has text content. Remove text content first, in order to provide HTML content instead.");

            _message.Html = htmlContent;

            return this;
        }

        /// <summary>
        /// Supplies <see cref="EmailitMessage.Text"/> property internally. 
        /// </summary>
        /// <returns>This <see cref="EmailitMessageBuilder"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="htmlContent"/> is null, empty or whitespace.</exception>
        /// <exception cref="InvalidOperationException">When <see cref="EmailitMessage"/> already has <see cref="EmailitMessage.Html"/> supplied.</exception>
        public EmailitMessageBuilder WithTextContent(string textContent)
        {
            if (string.IsNullOrWhiteSpace(textContent))
                throw new ArgumentNullException(nameof(textContent));
            if (!string.IsNullOrWhiteSpace(_message.Html))
                throw new InvalidOperationException("Emailit Message already has HTML content. Remove HTML content first, in order to provide text content instead.");

            _message.Text = textContent;

            return this;
        }
    }
}
