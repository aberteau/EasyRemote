using System;
using System.Net;
using System.Text;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Net.Http.Utilities;

namespace Techeasy.MicroFramework.Net.Http
{
    class HttpHandler
    {
        private HttpListenerContext context;

        public HttpHandler(HttpListenerContext context)
        {
            this.context = context;
        }

        public void Handle()
        {
            String query = HttpUtility.ExtractQuery(context.Request.Url.OriginalString);
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
            string msg = context.Request.HttpMethod + " " + context.Request.Url.OriginalString + "<br/>" + GetHtmlDebugTable(nameValueCollection);
            String str = "<html><body>" + msg + "</body></html>";
            byte[] messageBody = Encoding.UTF8.GetBytes(str);
            context.Response.ContentType = "text/html";
            context.Response.ContentLength64 = messageBody.Length;
            context.Response.OutputStream.Write(messageBody, 0, messageBody.Length);
            context.Response.OutputStream.Close();
        }

        private String GetHtmlDebugTable(NameValueCollection nameValueCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table>");
            for (int i = 0; i < nameValueCollection.Pairs.Length; i++)
            {
                NameValuesPair pair = nameValueCollection.Pairs[i];
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td>" + pair.Name + "</td>");
                stringBuilder.Append("<td>");
                for (int j = 0; j < pair.Values.Length; j++)
                {
                    stringBuilder.Append(pair.Values[j]);
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append("</td>");
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }
    }
}
