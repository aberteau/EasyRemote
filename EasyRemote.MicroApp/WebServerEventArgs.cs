using System;
using System.Net.Sockets;
using Microsoft.SPOT;

namespace Techeasy.EasyRemote.MicroApp
{
    public class WebServerEventArgs
        : EventArgs
    {
        public WebServerEventArgs(Socket response, string rawUrl)
        {
            Response = response;
            RawUrl = rawUrl;
        }

        public Socket Response { get; protected set; }
        public string RawUrl { get; protected set; }

    }
}
