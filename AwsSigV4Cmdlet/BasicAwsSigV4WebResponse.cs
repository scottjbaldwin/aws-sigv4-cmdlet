using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AwsSigV4Cmdlet
{
    /// <summary>
    /// <para type="description">The response output from a request.</para>
    /// </summary>
    public class BasicAwsSigV4WebResponse
    {
        /// <summary>
        /// <para type="description">The returned status code.</para>
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// <para type="description">The status code description.</para>
        /// </summary>
        public string StatusCodeDescription { get; set; } = string.Empty;

        /// <summary>
        /// <para type="description">The content returned from the request.</para>
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// <para type="description">The raw content returned from the request.</para>
        /// </summary>
        public string RawContent { get; set; } = string.Empty;

        /// <summary>
        /// <para type="description">The response headers returned by the request.</para>
        /// </summary>
        public Dictionary<string, string>? Headers { get; set; }

        /// <summary>
        /// <para type="description">The Raw content length.</para>
        /// </summary>
        public int RawContentLength { get; set; }
    }
}
