using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Net.NetworkInformation;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Techeasy.MicroFramework.Net.Http;

namespace Techeasy.EasyRemote.MicroApp
{
    public class Program
    {
        public static void Main()
        {
            Debug.EnableGCMessages(false);
            Debug.Print("Web Server test software");

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface networkInterface = networkInterfaces[0];
            networkInterface.EnableDhcp();
            Debug.Print("IP Address = " + networkInterface.IPAddress + ", Gateway = " + networkInterface.GatewayAddress + ", MAC = " + networkInterface.PhysicalAddress);

            var httpServer = new HttpServer();
            //webServer.Add(new RequestRoute("/test", HttpMethod.GET, request => new HtmlResponse("Hello World !")));
            //webServer.Add(new RequestRoute("/api/time", HttpMethod.GET, GetTime));
            //webServer.Add(new RequestRoute("/api/time", HttpMethod.PUT, SetTime));
            httpServer.RunAsync();
            Thread.Sleep(Timeout.Infinite);
        }

        //private static void GetTime(HttpListenerContext context)
        //{
        //    string msg = context.Request.HttpMethod + " " + context.Request.Url;
        //    String str = "<html><body><h1>" + msg + "</h1></body></html>";
        //    byte[] messageBody = Encoding.UTF8.GetBytes(str);
        //    context.Response.ContentType = "text/html";
        //    context.Response.ContentLength64 = messageBody.Length;
        //    context.Response.OutputStream.Write(messageBody, 0, messageBody.Length);
        //    //context.Response.OutputStream.Close();
        //}

        //private static void SetTime(HttpListenerContext context)
        //{
        //    return new EmptyResponse();
        //}
    }
}
