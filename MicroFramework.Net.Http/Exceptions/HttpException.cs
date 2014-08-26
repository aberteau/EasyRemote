using System;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class HttpException
        : Exception
    {
        public int HttpCode { get; private set; }

        public HttpException(int httpCode, String message, Exception innerException)
            : base(message, innerException)
        {
            HttpCode = httpCode;
        }

        public HttpException(int httpCode, String message)
            : this(httpCode, message, null)
        { }
    }
}
