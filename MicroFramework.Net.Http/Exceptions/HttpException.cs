using System;
using System.Net;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class HttpException
        : Exception
    {
        public HttpStatusCode HttpCode { get; private set; }

        public Int32 HttpCodeInt
        {
            get { return (Int32) HttpCode; }
        }

        public HttpException(HttpStatusCode httpCode, String message, Exception innerException)
            : base(message, innerException)
        {
            HttpCode = httpCode;
        }

        public HttpException(HttpStatusCode httpCode, String message)
            : this(httpCode, message, null)
        { }
    }
}
