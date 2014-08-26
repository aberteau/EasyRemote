using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Json.NETMF;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Net.NetworkInformation;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Techeasy.EasyRemote.MicroApp.Components;
using Techeasy.MicroFramework.Library;
using Techeasy.MicroFramework.Net.Http;
using Techeasy.MicroFramework.Net.Http.Exceptions;
using Techeasy.MicroFramework.Net.Http.Requests;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.EasyRemote.MicroApp
{
    public class Program
    {
        private const float AnalogReference = 3.3f;

        private static OutputPort _led;

        private static PowerOutletStrip _powerOutletStrip;

        private static AnalogInput _photoResistorPort;

        private static bool _ledStatus;

        public static void Main()
        {
            Debug.EnableGCMessages(false);
            Debug.Print("Web Server test software");

            _led = new OutputPort(Pins.ONBOARD_LED, false);

            _photoResistorPort = new AnalogInput(Cpu.AnalogChannel.ANALOG_0);

            _powerOutletStrip = new PowerOutletStrip();
            _powerOutletStrip.AddOutlet(1, new OutputPort(Pins.GPIO_PIN_D8, false));
            _powerOutletStrip.AddOutlet(2, new OutputPort(Pins.GPIO_PIN_D9, false));
            _powerOutletStrip.AddOutlet(3, new OutputPort(Pins.GPIO_PIN_D10, false));

            DebugNetworkInfo();

            RouteDispatcher routeDispatcher = InitRouteDispatcher();

            var httpServer = new HttpServer();
            httpServer.AddHttpHandler(routeDispatcher);
            httpServer.RunAsync();
            Thread.Sleep(Timeout.Infinite);
        }

        private static void DebugNetworkInfo()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface networkInterface = networkInterfaces[0];
            Debug.Print("IP Address = " + networkInterface.IPAddress + ", Gateway = " + networkInterface.GatewayAddress + ", MAC = " + networkInterface.PhysicalAddress);
        }

        private static RouteDispatcher InitRouteDispatcher()
        {
            var routeDispatcher = new RouteDispatcher();
            routeDispatcher.Add(new Route("/time", HttpMethod.Get, GetTime));
            routeDispatcher.Add(new Route("/led", HttpMethod.Get, ChangeLedStatus));
            routeDispatcher.Add(new Route("/debugquery", HttpMethod.Get, DebugQuery));
            routeDispatcher.Add(new Route("/outlet", HttpMethod.Get, ChangeOutletStatus));
            routeDispatcher.Add(new Route("/luminosite", HttpMethod.Get, GetLuminosite));
            return routeDispatcher;
        }

        private static void GetLuminosite(HttpListenerRequest request, HttpListenerResponse response)
        {
            double voltagePercentage = _photoResistorPort.Read();
            double analogValue = voltagePercentage * AnalogReference;

            string json = JsonSerializer.SerializeObject(analogValue);
            byte[] messageBody = Encoding.UTF8.GetBytes(json);
            response.ContentType = "application/json";
            response.ContentLength64 = messageBody.Length;
            response.OutputStream.Write(messageBody, 0, messageBody.Length);
            response.OutputStream.Close();
        }

        private static void ChangeOutletStatus(HttpListenerRequest request, HttpListenerResponse response)
        {
            Url url = HttpUtility.ExtractUrl(request.Url.OriginalString);
            UInt16 outletNum = 0;
            bool outletState = false;

            try
            {
                outletNum = url.Params.GetSingleValueUInt16("n");
            }
            catch (Exception exception)
            {
                throw new BadRequestHttpException("N° de prise fournie incorrect", exception);
            }

            if(outletNum == 0)
                throw new BadRequestHttpException("N° de prise doit être différent de 0");

            try
            {
                outletState = url.Params.GetSingleValueBoolean("s");
            }
            catch (Exception exception)
            {
                throw new BadRequestHttpException("Etat de la prise incorrect", exception);
            }

            _powerOutletStrip.Write(outletNum, outletState);
            response.StatusCode = (Int32) HttpStatusCode.OK;
        }

        private static void DebugQuery(HttpListenerRequest request, HttpListenerResponse response)
        {
            Url url = HttpUtility.ExtractUrl(request.Url.OriginalString);

            string json = JsonSerializer.SerializeObject(url.Params.ToHashtable());
            byte[] messageBody = Encoding.UTF8.GetBytes(json);
            response.ContentType = "application/json";
            response.ContentLength64 = messageBody.Length;
            response.OutputStream.Write(messageBody, 0, messageBody.Length);
            response.OutputStream.Close();
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


