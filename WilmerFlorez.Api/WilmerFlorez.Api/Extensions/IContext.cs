using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WilmerFlorez.Api.Extensions
{
    public interface IContext
    {
        string GetAbsoluteUrl();
    }

    public class Context : IContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        public Context(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Port = request.Host.Port ?? 0;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        // Similar methods for Url/AbsolutePath which internally call GetAbsoluteUri
        public string GetAbsoluteUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var url = $"{request.Scheme}://{request.Host.Host}";
            if ((request.Host.Port ?? 0) > 0)
            {
                url = $"{request.Scheme}://{request.Host.Host}:{request.Host.Port ?? 0}";
            }
            return url;
        }
    }

}
