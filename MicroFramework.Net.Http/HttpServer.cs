using System;
using System.Collections;
using System.Net;
using System.Threading;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http
{
    public class HttpServer
    {
        private readonly HttpListener _httpListener;

        private readonly ArrayList _httpHandlers;

        public HttpServer()
        {
            _httpListener = new HttpListener("http");
            _httpHandlers = new ArrayList();
        }

        public void AddHttpHandler(IHttpHandler httpHandler)
        {
            _httpHandlers.Add(httpHandler);
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
                new Thread(() => Handle(ctx)).Start();
            }
        }

        private void Handle(HttpListenerContext ctx)
        {
            foreach (var httpHandlerObj in _httpHandlers)
            {
                IHttpHandler httpHandler = httpHandlerObj as IHttpHandler;
                httpHandler.ProcessRequest(ctx);
            }
            ctx.Close();
        }
    }
}