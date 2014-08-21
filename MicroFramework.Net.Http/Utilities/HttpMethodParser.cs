using System;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Utilities
{
    public static class HttpMethodParser
    {
        public static HttpMethod Parse(string method)
        {
            switch (method)
            {
                case "GET": return HttpMethod.Get;
                case "POST": return HttpMethod.Post;
                case "DELETE": return HttpMethod.Delete;
                case "PUT": return HttpMethod.Put;
                default: throw new ApplicationException("Unknown Http Method");
            }
        }
    }
}
