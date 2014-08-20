using System;
using Microsoft.SPOT;

namespace Techeasy.Framework.MicroWebServer.Utilities
{
    public static class HttpMethodParser
    {
        public static HttpMethod Parse(string method)
        {
            switch (method)
            {
                case "GET": return HttpMethod.GET;
                case "POST": return HttpMethod.POST;
                case "DELETE": return HttpMethod.DELETE;
                case "PUT": return HttpMethod.PUT;
                default: throw new ApplicationException("Unknown Http Method");
            }
        }
    }
}
