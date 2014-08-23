using System;
using System.Collections;
using System.Net;
using System.Text;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Library;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http
{
    public class RouteDispatcher
        : IHttpHandler
    {
        private readonly Hashtable _routeTable;

        public RouteDispatcher()
        {
            _routeTable = new Hashtable();
        }

        public void Add(Route route)
        {
            _routeTable.Add(GetKey(route.HttpMethod, route.Path), route);
        }

        private bool Contains(HttpMethod httpMethod, string path)
        {
            return _routeTable.Contains(GetKey(httpMethod, path));
        }

        private static string GetKey(HttpMethod httpMethod, string path)
        {
            return httpMethod + "_" + path;
        }

        private Route Find(HttpMethod httpMethod, string path)
        {
            if (!Contains(httpMethod, path))
                return null;

            return (Route)_routeTable[GetKey(httpMethod, path)];
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            Url url = HttpUtility.ExtractUrl(context.Request.Url.OriginalString);
            var method = HttpMethodParser.Parse(context.Request.HttpMethod);
            var route = Find(method, url.Path);

            if (route != null)
                route.RequestHandler(context);

            context.Close();
        }
    }
}
