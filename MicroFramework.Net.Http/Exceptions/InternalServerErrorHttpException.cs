using System;
using System.Net;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class InternalServerErrorHttpException
        : HttpException
    {
        public InternalServerErrorHttpException(string message, Exception innerException)
            : base((Int32)HttpStatusCode.InternalServerError, message, innerException)
        { }

        public InternalServerErrorHttpException(string message)
            : this(message, null)
        { }
    }
}
