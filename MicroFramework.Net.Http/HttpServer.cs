using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Threading;

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
            catch(Exception exception)
            {
                byte[] messageBody = Encoding.UTF8.GetBytes(exception.ToString());
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;
                context.Response.ContentLength64 = messageBody.Length;
                context.Response.OutputStream.Write(messageBody, 0, messageBody.Length);
                context.Response.OutputStream.Close();
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