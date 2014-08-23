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
        static readonly ArrayList HttpHandlers = new ArrayList();

        public void AddHttpHandler(IHttpHandler httpHandler)
        {
            HttpHandlers.Add(httpHandler);
        }

        public void RunAsync()
        {
            new Thread(Run).Start();
        }

        private void Run()
        {
            HttpListener httpListener = new HttpListener("http");
            while (true)
            {
                try
                {
                    if (!httpListener.IsListening)
                        httpListener.Start();

                    HttpListenerContext context = httpListener.GetContext();
                    Thread th = new Thread(new ThreadStart(() => HandleRequestThread(context)));
                    th.Start();
                }
                catch (InvalidOperationException)
                {
                    httpListener.Stop();
                    Thread.Sleep(1000);
                }
                catch (ObjectDisposedException)
                {
                    httpListener.Start();
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private static void HandleRequestThread(HttpListenerContext context)
        {
            try
            {
                foreach (var httpHandlerObj in HttpHandlers)
                {
                    IHttpHandler httpHandler = httpHandlerObj as IHttpHandler;
                    httpHandler.ProcessRequest(context);
                }
            }
            catch
            {
            }
            finally
            {
                if (context != null)
                {
                    context.Close();
                }
            }
        }
    }
}