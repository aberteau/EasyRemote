using System;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Library;

namespace Techeasy.MicroFramework.Net.Http
{
    public class Url
    {
        public String Path { get; private set; }

        public NameValueCollection Params { get; private set; }

        public Url(String path, NameValueCollection @params)
        {
            Path = path;
            Params = @params;
        }
    }
}
