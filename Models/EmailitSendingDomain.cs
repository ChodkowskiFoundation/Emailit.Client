using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Emailit.Client.Models
{
    public class EmailitSendingDomain
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dns_checked_at")]
        public DateTime? DnsCheckedAtUtc { get; set; }

        [JsonPropertyName("spf_status")]
        public string SpfStatus { get; set; }

        [JsonPropertyName("spf_error")]
        public string SpfError { get; set; }

        [JsonPropertyName("dkim_status")]
        public string DkimStatus { get; set; }

        [JsonPropertyName("dkim_error")]
        public string DkimError { get; set; }

        [JsonPropertyName("mx_status")]
        public string MxStatus { get; set; }

        [JsonPropertyName("mx_error")]
        public string MxError { get; set; }

        [JsonPropertyName("dmarc_status")]
        public string DmarcStatus { get; set; }

        [JsonPropertyName("dmarc_error")]
        public string DmarcError { get; set; }

        [JsonPropertyName("return_path_status")]
        public string ReturnPathStatus { get; set; }

        [JsonPropertyName("return_path_error")]
        public string ReturnPathError { get; set; }

        [JsonPropertyName("last_used_at")]
        public DateTime? LastUsedAtUtc { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAtUtc { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAtUtc { get; set; }

        [JsonPropertyName("dns_records")]
        public IEnumerable<EmailitDnsRecord> DnsRecords { get; set; }

        [JsonPropertyName("track_loads")]
        public int? TrackLoads { get; set; }

        [JsonPropertyName("track_clicks")]
        public int? TrackClicks { get; set; }
    }
}
