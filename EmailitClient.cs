using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Emailit.Client.Enums;
using Emailit.Client.Extensions;
using Emailit.Client.Json;
using Emailit.Client.Models;
using Emailit.Client.Requests;
using Emailit.Client.Responses;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace Emailit.Client
{
    public class EmailitClient
    {
        protected const string DEFAULT_BASE_URL = "https://api.emailit.com";
        protected const string DEFAULT_VERSION = "v1";

        protected const string CREDENTIALS_PATH = "credentials";
        protected const string EMAIL_PATH = "emails";
        protected const string SENDIND_DOMAINS_PATH = "sending-domains";
        protected const string SENDIND_DOMAINS_CHECK_DNS_PATH = "check";

        protected string _apiKey;
        protected string _baseUrlString;
        protected string _version;

        protected JsonSerializerOptions _jsonSerializerOptions = null;
        protected readonly JsonSerializerOptions _defaultJsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new CaseInsensitiveEnumConverter<EmailitCredentialType>() },
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        };

        public EmailitClient(string apiKey) : this(new EmailitConfiguration { ApiKey = apiKey }) { }

        public EmailitClient(EmailitConfiguration configuration = null)
        {
            Configure(configuration);
        }

        public HttpCompletionOption DefualtHttpCompletionOption { get; set; }

        public JsonSerializerOptions JsonSerializerOptions => _jsonSerializerOptions ?? _defaultJsonSerializerOptions;

        public EmailitClient Configure(
            EmailitConfiguration configuration = null,
            JsonSerializerOptions jsonSerializerOptions = null)
        {
            _apiKey = configuration?.ApiKey;

            _baseUrlString = string.IsNullOrWhiteSpace(configuration?.BaseUrl)
                ? DEFAULT_BASE_URL
                : configuration.BaseUrl;

            _version = string.IsNullOrWhiteSpace(configuration?.Version)
                ? DEFAULT_VERSION
                : configuration.Version;

            DefualtHttpCompletionOption = configuration?.HttpCompletionOption ?? default;

            if (jsonSerializerOptions != null)
            {
                return ConfigureJsonSerializerOptions(jsonSerializerOptions);
            }
            else
            {
                return this;
            }
        }

        public EmailitClient ConfigureJsonSerializerOptions(
            JsonSerializerOptions options)
        {
            _jsonSerializerOptions = options;

            return this;
        }

        public EmailitClient Configure(Action<EmailitConfiguration> configureAction)
        {
            var configuration = new EmailitConfiguration();
            configureAction.Invoke(configuration);
            return Configure(configuration);
        }

        public Task<EmailitData<EmailitSendingDomain>> CheckSendingDomainDnsAsync(
            int sendingDomainId,
            CancellationToken cancellationToken = default)
            => DefaultBaseUrl
            .AppendPathSegments(SENDIND_DOMAINS_PATH, sendingDomainId, SENDIND_DOMAINS_CHECK_DNS_PATH)
            .PostJsonAsync(null, DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<EmailitData<EmailitSendingDomain>>();

        public Task<EmailitData<EmailitCredential>> CreateCredentialAsync(
            string name,
            EmailitCredentialType credentialType,
            CancellationToken cancellationToken = default) 
            => DefaultBaseUrl
            .AppendPathSegment(CREDENTIALS_PATH)
            .PostJsonAsync(new { 
                name, type = credentialType.ToString().ToLower() 
            }, DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<EmailitData<EmailitCredential>>();

        public Task<CreateSendingDomainResult> CreateSendingDomainAsync(
            string name,
            CancellationToken cancellationToken = default)
            => DefaultBaseUrl
            .AppendPathSegment(SENDIND_DOMAINS_PATH)
            .PostJsonAsync(new { name }, DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<CreateSendingDomainResult>();

        public Task<EmailitResult> DeleteCredentialAsync(
            int credentialId,
            CancellationToken cancellationToken = default)
            => DefaultBaseUrl
            .AppendPathSegments(CREDENTIALS_PATH, credentialId)
            .DeleteAsync(DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<EmailitResult>();

        public Task<EmailitResult> DeleteSendingDomainAsync(
            int sendingDomainId,
            CancellationToken cancellationToken = default)
            => DefaultBaseUrl
            .AppendPathSegments(SENDIND_DOMAINS_PATH, sendingDomainId)
            .DeleteAsync(DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<EmailitResult>();

        public Task<EmailitData<EmailitCredential[]>> GetAllCredentialsAsync(
            EmailitFilter filter = null,
            CancellationToken cancellationToken = default)
        {
            filter ??= new EmailitFilter();

            return DefaultBaseUrl
                .AppendPathSegment(CREDENTIALS_PATH)
                .SendJsonAsync(HttpMethod.Get, filter, DefualtHttpCompletionOption, cancellationToken)
                .ReceiveJson<EmailitData<EmailitCredential[]>>();
        }

        public Task<EmailitData<EmailitSendingDomain[]>> GetAllSendingDomainsAsync(
            EmailitFilter filter = null,
            CancellationToken cancellationToken = default)
        {
            filter ??= new EmailitFilter();

            return DefaultBaseUrl
                .AppendPathSegment(SENDIND_DOMAINS_PATH)
                .SendJsonAsync(HttpMethod.Get, filter, DefualtHttpCompletionOption, cancellationToken)
                .ReceiveJson<EmailitData<EmailitSendingDomain[]>>();
        }

        public Task<EmailitData<EmailitCredential>> GetCredentialAsync(
            int credentialId, 
            CancellationToken cancellationToken = default) 
            => DefaultBaseUrl
            .AppendPathSegments(CREDENTIALS_PATH, credentialId)
            .GetJsonAsync<EmailitData<EmailitCredential>>(DefualtHttpCompletionOption, cancellationToken);

        public Task<EmailitData<EmailitSendingDomain>> GetSendingDomainAsync(
            int sendingDomainId,
            CancellationToken cancellationToken = default)
            => DefaultBaseUrl
            .AppendPathSegments(SENDIND_DOMAINS_PATH, sendingDomainId)
            .GetJsonAsync<EmailitData<EmailitSendingDomain>>(DefualtHttpCompletionOption, cancellationToken);

        public Task<EmailitData<SendEmailResponse>>SendEmailAsync(
            EmailitMessage message, 
            CancellationToken cancellationToken = default) 
            => DefaultBaseUrl
            .AppendPathSegment(EMAIL_PATH)
            .PostJsonAsync(message, DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<EmailitData<SendEmailResponse>>();

        public Task<EmailitData<EmailitCredential>> UpdateCredentialName(
            int credentialId,
            string name,
            CancellationToken cancellationToken = default)
            => DefaultBaseUrl
            .AppendPathSegments(CREDENTIALS_PATH, credentialId)
            .PutJsonAsync(new { name }, DefualtHttpCompletionOption, cancellationToken)
            .ReceiveJson<EmailitData<EmailitCredential>>();

        public EmailitClient UseApiKey(string apiKey)
        {
            _apiKey = apiKey;
            
            return this;
        }

        public EmailitClient UseBaseUrl(string baseUrl)
        {
            _baseUrlString = baseUrl ?? DEFAULT_BASE_URL;

            return this;
        }

        public EmailitClient UseConfiguration(EmailitConfiguration configuration)
        {
            _apiKey = configuration?.ApiKey;
            _baseUrlString = configuration?.BaseUrl ?? DEFAULT_BASE_URL;
            _version = configuration?.Version ?? DEFAULT_VERSION;

            return this;
        }

        public EmailitClient UseVersion(string version)
        {
            _version = version;

            return this;
        }

        protected virtual IFlurlRequest DefaultBaseUrl => new Url(_baseUrlString)
            .WithSettings(s =>
            {
                s.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
            })
            .WithOAuthBearerToken(_apiKey)
            .EnsureApiKeyIsProvided()
            .AppendPathSegment(_version);
    }
}
