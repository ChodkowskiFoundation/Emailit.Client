using System;

using Flurl.Http;

namespace Emailit.Client.Extensions
{
    internal static class FlurlRequestExtensions
    {
        public static IFlurlRequest EnsureApiKeyIsProvided(this IFlurlRequest request)
        {
            if (request.Headers.TryGetFirst("Authorization", out string value))
            {
                var splittedAuthHeader = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string apiKey = splittedAuthHeader.Length > 1 
                    ? splittedAuthHeader[1] 
                    : null;

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    throw new InvalidOperationException("Emailit Api Key not provided.");
                }
            }

            return request;
        }
    }
}
