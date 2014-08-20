using System;
using System.Net;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.Framework.MicroWebServer
{
    class Worker
    {
        private HttpListenerContext context;

        public Worker(HttpListenerContext context)
        {
            this.context = context;
        }

        public void ProcessRequest()
        {
            string msg = context.Request.HttpMethod + " " + context.Request.Url.OriginalString;
            String str = "<html><body>" + msg + "</body></html>";
            byte[] messageBody = Encoding.UTF8.GetBytes(str);
            context.Response.ContentType = "text/html";
            context.Response.ContentLength64 = messageBody.Length;
            context.Response.OutputStream.Write(messageBody, 0, messageBody.Length);
            context.Response.OutputStream.Close();
        }
    }
}
