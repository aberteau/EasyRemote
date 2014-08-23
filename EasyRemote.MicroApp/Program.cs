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
using Techeasy.MicroFramework.Library;
using Techeasy.MicroFramework.Net.Http;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.EasyRemote.MicroApp
{
    public class Program
    {
        private static OutputPort _led;

        private static bool _ledStatus;

        public static void Main()
        {
            Debug.EnableGCMessages(false);
            Debug.Print("Web Server test software");

            _led = new OutputPort(Pins.ONBOARD_LED, false);

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface networkInterface = networkInterfaces[0];
            networkInterface.EnableDhcp();
            Debug.Print("IP Address = " + networkInterface.IPAddress + ", Gateway = " + networkInterface.GatewayAddress + ", MAC = " + networkInterface.PhysicalAddress);

            RouteDispatcher routeDispatcher = InitRouteDispatcher();

            var httpServer = new HttpServer();
            httpServer.AddHttpHandler(routeDispatcher);
            httpServer.RunAsync();
            Thread.Sleep(Timeout.Infinite);
        }

        private static RouteDispatcher InitRouteDispatcher()
        {
            var routeDispatcher = new RouteDispatcher();
            routeDispatcher.Add(new Route("/api/time", HttpMethod.Get, GetTime));
            routeDispatcher.Add(new Route("/api/led", HttpMethod.Get, ChangeLedStatus));
            return routeDispatcher;
        }

        private static void ChangeLedStatus(HttpListenerRequest request, HttpListenerResponse response)
        {
            _ledStatus = !_ledStatus;
            _led.Write(_ledStatus);
        }

        private static void GetTime(HttpListenerRequest request, HttpListenerResponse response)
        {
            Url url = HttpUtility.ExtractUrl(request.Url.OriginalString);
            string msg = request.HttpMethod + " " + request.Url.OriginalString + "<br/>" + GetHtmlDebugTable(url.Params);
            String str = "<html><body>" + msg + "</body></html>";
            byte[] messageBody = Encoding.UTF8.GetBytes(str);
            response.ContentType = "text/html";
            response.ContentLength64 = messageBody.Length;
            response.OutputStream.Write(messageBody, 0, messageBody.Length);
            response.OutputStream.Close();
        }

        private static String GetHtmlDebugTable(NameValueCollection nameValueCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (nameValueCollection != null)
            {
                stringBuilder.Append("<table>");
                for (int i = 0; i < nameValueCollection.Pairs.Length; i++)
                {
                    NameValuesPair pair = nameValueCollection.Pairs[i];
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td>" + pair.Name + "</td>");
                    stringBuilder.Append("<td>");
                    for (int j = 0; j < pair.Values.Length; j++)
                    {
                        stringBuilder.Append(pair.Values[j]);
                        stringBuilder.Append(", ");
                    }
                    stringBuilder.Append("</td>");
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("</table>");    
            }

            return stringBuilder.ToString();
        }
    }
}


