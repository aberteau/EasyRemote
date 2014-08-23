using System;
using System.Collections;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http.Requests
{
    public class RequestRouteList : IEnumerable
    {
        private readonly Hashtable _routeTable;

        public RequestRouteList()
        {
            _routeTable = new Hashtable();
        }

        public IEnumerator GetEnumerator()
        {
            return _routeTable.GetEnumerator();
        }

        public void Add(RequestRoute route)
        {
            _routeTable.Add(GetKey(route.HttpMethod, route.Path), route);
        }

        public bool Contains(HttpMethod httpMethod, string path)
        {
            return _routeTable.Contains(GetKey(httpMethod, path));
        }

        private static string GetKey(HttpMethod httpMethod, string path)
        {
            return httpMethod + "_" + path;
        }

        public RequestRoute Find(HttpMethod httpMethod, string path)
        {
            if (!Contains(httpMethod, path))
                return null;

            return (RequestRoute)_routeTable[GetKey(httpMethod, path)];
        }
    }
}
