using System.Text.Json.Serialization;

namespace Emailit.Client.Responses
{
    public class EmailitData<T> where T : class
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("total_records")]
        public int? TotalRecords { get; set; }
    }
}
