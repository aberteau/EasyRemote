using System;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http.Requests
{
    public class Route
    {
        public string Path { get; private set; }
        public HttpRequestHandler RequestHandler { get; private set; }
        public HttpMethod HttpMethod { get; set; }
        public string ContentType { get; private set; }

        public Route(string path, HttpMethod httpMethod, HttpRequestHandler requestHandler)
        {
            Path = path;
            HttpMethod = httpMethod;
            RequestHandler = requestHandler;
        }
    }
}
