using System;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Library
{
    public static class NameValueCollectionExtensions
    {
        public static String ToTableHtmlString(this NameValueCollection nameValueCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (nameValueCollection != null)
            {
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
            }

            return stringBuilder.ToString();
        }
    }
}
