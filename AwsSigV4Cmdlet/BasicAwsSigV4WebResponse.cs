using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AwsSigV4Cmdlet
{
    public class BasicAwsSigV4WebResponse
    {
        public int StatusCode { get; set; }

        public string StatusCodeDescription { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string RawContent { get; set; } = string.Empty;

        public Dictionary<string, string>? Headers { get; set; }

        public int RawContentLength { get; set; }
    }
}
