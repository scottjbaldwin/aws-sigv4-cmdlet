using Amazon.Runtime;
using System;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AwsSigV4Cmdlet
{
    /// <summary>
    /// <para type="synopsis">Web Requests using AWS SigV4.</para>
    /// <para type="description">Makes a signed web request to a uri with AWS Signature Version 4 signing process for APIs that use AWS IAM authentication.</para> 
    /// <example>
    ///     <code>Invoke-AwsSigV4WebRequest -Uri https://foo.com/api/values -AccessKey AKIAXXXXXXXXXXXX -SecretKey XXXXXXXXXXXXXXXXXXXXXXXXXXXXX -Region us-east-1</code>
    ///     <para>Make a GET request on the foo.com api to get the values</para>
    /// </example>
    /// <example>
    ///     <code>Get-Content .\requestbody.json | Invoke-AwsSigV4WebRequest -Uri https://foo.com/api/values -Method POST -AccessKey AKIAXXXXXXXXXXXX -SecretKey XXXXXXXXXXXXXXXXXXXXXXXXXXXXX -Region us-east-1</code>
    ///     <para>Uses the contents of the file requestbody.json as the Body of the POST request to the foo.com api</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Invoke, "AwsSigV4WebRequest")]
    [OutputType(typeof(BasicAwsSigV4WebResponse))]
    public class InvokeAwsSigV4WebRequest : Cmdlet
    {
        /// <summary>
        /// <para type="description">The Uri to make the signed request to.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Uri { get; set; }

        /// <summary>
        /// <para type="description">The Body of the request.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// <para type="description">The access key of the IAM user.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string AccessKey { get; set; }

        /// <summary>
        /// <para type="description">The secret key of the IAM user.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string SecretKey { get; set; }

        /// <summary>
        /// <para type="description">The session token when using temproary credentials.</para>
        /// </summary>
        [Parameter()]
        public string Token { get; set; }

        /// <summary>
        /// <para type="description">The HTTP method to invoke.</para>
        /// </summary>
        [Parameter()]
        [ValidateSet("GET", "POST", "PUT", "DELETE")]
        public string Method { get; set; } = "GET";

        /// <summary>
        /// <para type="description">The AWS region the API is hosted in.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Region { get; set; }

        /// <summary>
        /// <para type="description">The AWS service you are invoking (e.g. "execute-api").</para>
        /// </summary>
        [Parameter()]
        public string Service { get; set; } = "execute-api";

        /// <summary>
        /// <para type="description">The http content type</para>
        /// </summary>
        [Parameter()]
        public string ContentType { get; set; } = "application/json";

        [Parameter()]
        public SwitchParameter SkipHttpErrorCheck { get; set; }

        private HttpClient _client;

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
                case "DELETE":
                    HandleDelete(credentials);
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

            if (ContentType == "application/json" || ContentType == "text/html" ||  ContentType == "text/plain")
            {
                var content = new StringContent(Body ?? string.Empty, System.Text.Encoding.UTF8, ContentType);
                var response = _client?.PutAsync(Uri, content, Region, Service, credentials).Result;

                if (response != null)
                {
                    ExtractOutput(response);
                }
            }
            else 
            {
                throw new NotImplementedException(ContentType);
            }
        }

        private void HandlePost(ImmutableCredentials credentials)
        {
            if (ContentType == "application/json" || ContentType == "text/html" || ContentType == "text/plain")
            {
                var content = new StringContent(Body ?? string.Empty, System.Text.Encoding.UTF8, ContentType);
                var response = _client?.PostAsync(Uri, content, Region, Service, credentials).Result;

                if (response != null)
                {
                    ExtractOutput(response);
                }
            }
            else 
            {
                throw new NotImplementedException(ContentType);
            }
        }

        private void HandleGet(ImmutableCredentials credentials)
        {
            var response = _client?.GetAsync(Uri, HttpCompletionOption.ResponseContentRead, Region, Service, credentials).Result;

            if (response != null)
            {
                ExtractOutput(response);
            }
        }

        private void HandleDelete(ImmutableCredentials credentials)
        {
            var response = _client?.DeleteAsync(Uri, Region, Service, credentials).Result;

            if (response != null)
            {
                ExtractOutput(response);
            }
        }

        private void ExtractOutput(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var output = new BasicAwsSigV4WebResponse
            {
                Content = content,
                StatusCode = (int)response.StatusCode,
                StatusCodeDescription = response.StatusCode.ToString(),
                Headers = response.Headers.ToDictionary(x => x.Key, x => string.Join(", ", x.Value)),
                ContentLength = content.Length
            };

            if (output.StatusCode < (int)HttpStatusCode.BadRequest || SkipHttpErrorCheck.IsPresent)
            {
                WriteObject(output);
            }
            else
            {
                var ex = new HttpRequestException($"{output.StatusCode} - {output.StatusCodeDescription}: {output.Content}");
                var errorRecord = new ErrorRecord(ex, output.StatusCode.ToString(), ErrorCategory.ProtocolError, output.Content);
                WriteError(errorRecord);
            }
        }
    }
}