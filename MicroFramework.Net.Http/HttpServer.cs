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
        private static ArrayList _httpHandlers;

        static Queue _responseQueue = new Queue();

        public HttpServer()
        {
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
            HttpListener httpListener = new HttpListener("http");
            while (true)
            {
                try
                {
                    if (!httpListener.IsListening)
                        httpListener.Start();

                    HttpListenerContext context = httpListener.GetContext();
                    lock (_responseQueue)
                    {
                        _responseQueue.Enqueue(context);
                    }

                    Thread th = new Thread(new ThreadStart(HandleRequestThread));
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

        private static void HandleRequestThread()
        {
            HttpListenerContext context = null;

            try
            {
                lock (_responseQueue)
                {
                    context = (HttpListenerContext)_responseQueue.Dequeue();
                }

                foreach (var httpHandlerObj in _httpHandlers)
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