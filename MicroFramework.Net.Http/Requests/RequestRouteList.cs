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
            _routeTable = new Hashtable(25);
        }

        public IEnumerator GetEnumerator()
        {
            return _routeTable.GetEnumerator();
        }

        public void Add(RequestRoute route)
        {
            if (route.IsFileResponse)
                _routeTable.Add(HttpMethod.Get + "_" + route.Path, route);
            else
                _routeTable.Add(route.HttpMethod + "_" + route.Path, route);
        }

        public bool Contains(HttpMethod httpMethod, string path)
        {
            return _routeTable.Contains(httpMethod + "_" + path);
        }

        public RequestRoute Find(HttpMethod httpMethod, string path)
        {
            return (RequestRoute)_routeTable[httpMethod + "_" + path];
        }
    }
}
