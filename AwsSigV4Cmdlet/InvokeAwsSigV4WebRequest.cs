using Amazon.Runtime;
using System.Management.Automation;

namespace AwsSigV4Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "AwsSigV4WebRequest")]
    [OutputType(typeof(HttpResponseMessage))]
    public class InvokeAwsSigV4WebRequest : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Uri { get; set; }

        [Parameter(ValueFromPipeline = true)] 
        public string? Body { get; set; }

        [Parameter()]
        public string? AccessKey { get; set; }

        [Parameter()]
        public string? SecretKey { get; set; }

        [Parameter()]
        public string? Token { get; set; }

        [Parameter()]
        [ValidateSet("GET", "POST", "PUT", "DELETE")]
        public string Method { get; set; }

        [Parameter()]
        public string Region { get; set; }

        [Parameter()]
        public string Service { get; set; }

        private HttpClient? _client;

        public InvokeAwsSigV4WebRequest()
        {
            if (Uri == null)
            {
                throw new ArgumentNullException(nameof(Uri));
            }
            if (Region == null)
            {
                throw new ArgumentNullException(nameof(Region));
            }
            if (Method == null)
            {
                Method = "GET";
            }
            if (Service == null)
            {
                Service = "execute-api";
            }
        }
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            _client = new HttpClient();
        }

        protected override void ProcessRecord()
        {
            var credentials = new ImmutableCredentials(AccessKey, SecretKey, Token);
            
            if (Method == "GET")
            {
                var response = _client?.GetAsync(Uri, HttpCompletionOption.ResponseContentRead, Region, Service, credentials).Result;
                return;
            }
            throw new NotImplementedException("Give me half a chance");

        }
    }
}