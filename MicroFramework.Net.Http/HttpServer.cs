using System;
using System.Net;
using System.Threading;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http
{
    public class HttpServer
    {
        private HttpListener _httpListener;

        public HttpServer()
        {
            _httpListener = new HttpListener("http");
        }

        public void RunAsync()
        {
            new Thread(Run).Start();
        }

        public void Run()
        {
            _httpListener.Start();
            while(true)
            {
                HttpListenerContext ctx = _httpListener.GetContext();
                new Thread(new HttpHandler(ctx).Handle).Start();
            }
        }
    }
}