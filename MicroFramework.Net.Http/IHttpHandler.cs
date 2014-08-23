using System;
using System.Net;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http
{
    public interface IHttpHandler
    {
        void ProcessRequest(HttpListenerContext context);
    }
}
