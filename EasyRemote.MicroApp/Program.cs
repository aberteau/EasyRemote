using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Techeasy.EasyRemote.MicroApp
{
    public class Program
    {
        private static WebServer server;

        public static void Main()
        {
            // Start the HTTP Server
            WebServer server = new WebServer(80, 10000);
            server.CommandReceived += new WebServer.GetRequestHandler(ProcessClientGetRequest);
            // Start the server.
            server.Start();


        }

    }
}
