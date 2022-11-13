using Amazon.Runtime;
using System.Management.Automation;
using System.Net.Http.Headers;

namespace AwsSigV4Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "AwsSigV4WebRequest")]
    [OutputType(typeof(BasicAwsSigV4WebResponse))]
    public class InvokeAwsSigV4WebRequest : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Uri { get; set; }

        [Parameter(ValueFromPipeline = true)]
        public string? Body { get; set; } = string.Empty;

        [Parameter(Mandatory = true)]
        public string AccessKey { get; set; }

        [Parameter(Mandatory = true)]
        public string SecretKey { get; set; }

        [Parameter()]
        public string? Token { get; set; }

        [Parameter()]
        [ValidateSet("GET", "POST", "PUT", "DELETE")]
        public string Method { get; set; } = "GET";

        [Parameter(Mandatory = true)]
        public string Region { get; set; }

        [Parameter()]
        public string Service { get; set; } = "execute-api";

        [Parameter()]
        public string ContentType { get; set; } = "application/json";

        private HttpClient? _client;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            _client = new HttpClient();
        }

        protected override void ProcessRecord()
        {
            var credentials = new ImmutableCredentials(AccessKey, SecretKey, Token);
            
            switch (Method)
            {
                case "GET":
                    HandleGet(credentials);
                    break;
                case "POST":
                    HandlePost(credentials);
                    break;
                case "PUT":
                    HandlePut(credentials);
                    break;
                default:
                    throw new NotImplementedException("Give me half a chance");
            }
        }

        protected override void EndProcessing()
        {
            _client?.Dispose();
            base.EndProcessing();
        }

        private void HandlePut(ImmutableCredentials credentials)
        {
            HttpContent? content;
            switch (ContentType)
            {
                case "application/json":
                case "text/html":
                case "text/plain":
                    content = new StringContent(Body ?? string.Empty, System.Text.Encoding.UTF8, ContentType);
                    break;
                default:
                    throw new NotImplementedException(ContentType);
            }

            var response = _client?.PutAsync(Uri, content, Region, Service, credentials).Result;

            if (response != null)
            {
                var raw = response.Content.ReadAsStringAsync().Result;
                ExtractOutput(response, raw);
            }
        }

        private void HandlePost(ImmutableCredentials credentials)
        {
            HttpContent? content;
            switch (ContentType)
            {
                case "application/json":
                case "text/html":
                case "text/plain":
                    content = new StringContent(Body ?? string.Empty, System.Text.Encoding.UTF8, ContentType);
                    break;
                default:
                    throw new NotImplementedException(ContentType);
            }

            var response = _client?.PostAsync(Uri, content, Region, Service, credentials).Result;

            if (response != null)
            {
                var raw = response.Content.ReadAsStringAsync().Result;
                ExtractOutput(response, raw);
            }
        }

        private void HandleGet(ImmutableCredentials credentials)
        {
            var response = _client?.GetAsync(Uri, HttpCompletionOption.ResponseContentRead, Region, Service, credentials).Result;

            if (response != null)
            {
                var raw = response.Content.ReadAsStringAsync().Result;
                ExtractOutput(response, raw);
            }
        }

        private void ExtractOutput(HttpResponseMessage? response, string raw)
        {
            var output = new BasicAwsSigV4WebResponse
            {
                RawContent = raw,
                Content = raw,
                StatusCode = (int)response.StatusCode,
                StatusCodeDescription = response.StatusCode.ToString(),
                Headers = response.Headers.ToDictionary(x => x.Key, x => string.Join(", ", x.Value)),
                RawContentLength = raw.Length
            };

            WriteObject(output);
        }
    }
}