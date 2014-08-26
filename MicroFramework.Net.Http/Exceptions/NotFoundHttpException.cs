using System;
using System.Net;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class NotFoundHttpException
        : HttpException
    {
        public NotFoundHttpException(string message, Exception innerException)
            : base(HttpStatusCode.NotFound, message, innerException)
        { }

        public NotFoundHttpException(string message)
            : this(message, null)
        { }
    }
}
