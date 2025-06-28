using System;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.StaticFiles;

namespace Emailit.Client.Models
{
    public class EmailitAttachment
    {
        public EmailitAttachment() {}

        public EmailitAttachment(string fileName, string content, string contentType = null)
        {
            FileName = fileName;
            Content = content;

            if (string.IsNullOrWhiteSpace(contentType) && !string.IsNullOrWhiteSpace(fileName)
                && !new FileExtensionContentTypeProvider().TryGetContentType(FileName, out var type))
                ContentType = type;
            else ContentType = contentType;

        }

        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Creates <see cref="EmailitAttachment"/> from <paramref name="bytes"/>.
        /// </summary>
        /// <param name="bytes">Attachment file as byte array.</param>
        /// <param name="fileName">File name with file extension.</param>
        /// <param name="contentType">File MIME content type (assumed from <paramref name="fileName"/> if not provided).</param>
        /// <returns>New <see cref="EmailitAttachment"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="bytes"/> or <paramref name="fileName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException">When <paramref name="contentType"/> is null or empty and it cannot be assumed from <paramref name="fileName"/>.</exception>
        public static EmailitAttachment FromByteArray(byte[] bytes, string fileName, string contentType = null)
        {

            if (bytes == null || bytes.Length < 1) throw new ArgumentNullException(nameof(bytes));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));


            if (string.IsNullOrWhiteSpace(contentType)
                || !new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var fileExtensionsContentType))
            {
                throw new ArgumentException(
                    $"{nameof(contentType)} is null or empty. Attempt to assume {nameof(contentType)} from {fileName} failed.");
            }

            return new EmailitAttachment
            {
                Content = Convert.ToBase64String(bytes),
                FileName = fileName,
                ContentType = fileExtensionsContentType
            };
        }
    }
}
