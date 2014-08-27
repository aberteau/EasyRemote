using System;
using System.Net;
using System.Text;
using Microsoft.SPOT;
using Json.NETMF;

namespace Techeasy.MicroFramework.Net.Http
{
    public static class HttpListenerResponseExtensions
    {
        public static void WriteJson(this HttpListenerResponse response, object obj)
        {
            string json = JsonSerializer.SerializeObject(obj);
            byte[] messageBody = Encoding.UTF8.GetBytes(json);
            response.ContentType = ContentType.ApplicationJson;
            response.ContentLength64 = messageBody.Length;
            response.OutputStream.Write(messageBody, 0, messageBody.Length);
            response.OutputStream.Close();
        }

        public static void WriteHtml(this HttpListenerResponse response, String str)
        {
            byte[] messageBody = Encoding.UTF8.GetBytes(str);
            response.ContentType = ContentType.ApplicationJson;
            response.ContentLength64 = messageBody.Length;
            response.OutputStream.Write(messageBody, 0, messageBody.Length);
            response.OutputStream.Close();
        }
    }
}
