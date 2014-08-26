using System;
using System.Net;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Exceptions
{
    public class ExceptionHelper
    {
        public static void RespondError(HttpListenerResponse httpListenerResponse, HttpException httpException)
        {
            byte[] messageBody = Encoding.UTF8.GetBytes(httpException.ToString());
            httpListenerResponse.ContentType = "text/plain";
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
