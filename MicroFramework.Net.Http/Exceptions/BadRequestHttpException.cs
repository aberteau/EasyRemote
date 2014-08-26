using System;
using System.Net;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class BadRequestHttpException
        : HttpException
    {
        public BadRequestHttpException(string message, Exception innerException)
            : base((Int32)HttpStatusCode.BadRequest, message, innerException)
        { }

        public BadRequestHttpException(string message)
            : this(message, null)
        { }
    }
}
