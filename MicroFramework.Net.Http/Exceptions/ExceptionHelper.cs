using System;
using System.Net;
using System.Text;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Library;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class ExceptionHelper
    {
        public static void RespondError(HttpListenerResponse httpListenerResponse, HttpException httpException)
        {
            StringBuilder stringBuilder = new StringBuilder(httpException.Message);

            if (httpException.InnerException != null)
                stringBuilder.Append(" Détails : " + httpException.InnerException.Message);

            byte[] messageBody = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            httpListenerResponse.ContentType = "text/plain";
            httpListenerResponse.ContentEncoding = Encoding.UTF8;
            httpListenerResponse.StatusCode = httpException.HttpCodeInt;
            httpListenerResponse.ContentLength64 = messageBody.Length;
            httpListenerResponse.OutputStream.Write(messageBody, 0, messageBody.Length);
            httpListenerResponse.OutputStream.Close();
        }

        public static void RespondError(HttpListenerResponse httpListenerResponse, Exception exception)
        {
            HttpException httpException = new HttpException(HttpStatusCode.InternalServerError, "Une erreur interne est survenue", exception);
            RespondError(httpListenerResponse, httpException);
        }
    }
}
