using Microsoft.AspNetCore.Http;
using System.Text;
using System;

namespace WilmerFlorez.Common.Audit
{
    public class AuditInfo
    {
        public HttpContext  Context { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            var uriBuilder = new UriBuilder();

            uriBuilder.Scheme = Context.Request.Scheme;
            uriBuilder.Host = Context.Request.Host.Host;
            uriBuilder.Path = Context.Request.Path.ToString();
            uriBuilder.Query = Context.Request.QueryString.ToString();

            var url =  uriBuilder.Uri.AbsolutePath;
            sb.AppendLine($"AUDIT LOG: [{Context?.Request.Method?.ToString() ?? "---"}]: {url}");

            return sb.ToString();
        }
    }
}
