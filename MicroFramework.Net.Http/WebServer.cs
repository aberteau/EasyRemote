using System;
using System.Net;
using System.Threading;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http
{
    public class WebServer
    {
        private HttpListener _httpListener;

        public WebServer()
        {
            _httpListener = new HttpListener("http");
        }

        public void Run()
        {
            _httpListener.Start();
            while(true)
            {
                HttpListenerContext ctx = _httpListener.GetContext();
                new Thread(new Worker(ctx).ProcessRequest).Start();
            }
        }
    }
}