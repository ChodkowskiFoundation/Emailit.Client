using System.Net.Http;

namespace Emailit.Client
{
    public class EmailitConfiguration
    {
        public string ApiKey { get; set; }

        public string BaseUrl { get; set; } 

        public string Version { get; set; }

        public HttpCompletionOption HttpCompletionOption { get; set; }
    }
}
