using System;
using System.Net;
using System.Threading;
using Microsoft.SPOT;
using Techeasy.Framework.MicroWebServer.Requests;
using Techeasy.Framework.MicroWebServer.Utilities;

namespace Techeasy.Framework.MicroWebServer
{
    public class WebServer
    {
        private readonly RequestRouteList _routeList;
        private Thread _mainThread;
        private HttpListener _httpListener;

        public WebServer()
        {
            _routeList = new RequestRouteList();
            _mainThread = new Thread(MainThreadHandler);
            _mainThread.Start();
        }

        public bool MainThreadIsAlive { get { return _mainThread.IsAlive; } }

        public ThreadState MainThreadStatus { get { return _mainThread.ThreadState; } }

        public void RestartMainThread()
        {
            _mainThread.Abort();
            Thread.Sleep(2000);
            _mainThread = new Thread(MainThreadHandler);
            _mainThread.Start();
        }

        public void Add(RequestRoute route)
        {
            _routeList.Add(route);
        }

        private void MainThreadHandler()
        {
            _httpListener = new HttpListener("http");
            bool abortRequested = false;
            _httpListener.Start();

            //TODO: Threading verhaal nog te bekijken !!
            while (!abortRequested)
            {
                try
                {
                    Debug.Print("Webserver while loop beginning");
                    HttpListenerContext context = _httpListener.GetContext();
                    String path = context.Request.Url.OriginalString;
                    HttpMethod method = HttpMethodParser.Parse(context.Request.HttpMethod);
                    
                    Debug.Print("Webserver incoming request : '" + context.Request.Url + "'");

                    RequestRoute route = _routeList.Find(method, path);

                    if (route != null)
                        route.RequestHandler(_httpListener.GetContext());

                    if (context.Response != null)
                        context.Response.Close();
                }
                catch (Exception exception)
                {
                    _httpListener.Stop();
                    _httpListener.Close();
                    abortRequested = true;
                }
            }
        }
    }
}