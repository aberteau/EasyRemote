using System;
using System.Net;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Requests
{
    public delegate void HttpRequestHandler(HttpListenerRequest request, HttpListenerResponse response);
}
