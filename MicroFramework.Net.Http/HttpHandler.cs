using System;
using System.Net;
using System.Text;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Library;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http
{
    public class HttpHandler
        : IHttpHandler
    {
        private readonly RequestRouteList _routeList;

        public HttpHandler()
        {
            _routeList = new RequestRouteList();
        }

        public void AddRoute(RequestRoute route)
        {
            _routeList.Add(route);
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            Url url = HttpUtility.ExtractUrl(context.Request.Url.OriginalString);
            var method = HttpMethodParser.Parse(context.Request.HttpMethod);
            var route = _routeList.Find(method, url.Path);

            if (route != null)
                route.RequestHandler(context);

            context.Close();
        }
    }
}
