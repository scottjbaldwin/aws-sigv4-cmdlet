using System.Management.Automation;

namespace AwsSigV4Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "AwsSigV4WebRequest")]
    [OutputType(typeof(HttpResponseMessage))]
    public class InvokeAwsSigV4WebRequest : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Uri { get; set; }

        [Parameter(Mandatory = false, Position = 1)] 
        public string Body { get; set; }

        [Parameter()]
        public string AccessKey { get; set; }

        [Parameter()]
        public string SecretKey { get; set; }

        [Parameter()]
        public string Token { get; set; }

        [Parameter()]
        [ValidateSet("GET", "POST", "PUT", "DELETE")]
        public string Method { get; set; }

        [Parameter()]
        public string Region { get; set; }

        [Parameter()]
        public string Service { get; set; }
    }
}