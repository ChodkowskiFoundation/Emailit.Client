using System.Text.Json.Serialization;

namespace Emailit.Client.Requests
{
    public class EmailitFilter
    {
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; } = 25;

        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("filter[name]")]
        public string NameFilter { get; set; }

        [JsonPropertyName("filter[type]")]
        public string TypeFilter { get; set; }
    }
}
