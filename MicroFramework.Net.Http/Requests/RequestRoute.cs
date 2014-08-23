using System;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http.Requests
{
    public class RequestRoute
    {
        public string Path { get; private set; }
        public WebRequestHandler RequestHandler { get; private set; }
        public HttpMethod HttpMethod { get; set; }
        public string ContentType { get; private set; }

        public RequestRoute(string path, HttpMethod httpMethod, WebRequestHandler requestHandler)
        {
            Path = path;
            HttpMethod = httpMethod;
            RequestHandler = requestHandler;
        }
    }
}
