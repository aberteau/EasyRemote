using System;
using System.Collections;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Utilities
{
    public static class HttpUtility
    {
        public static String ExtractQuery(String url)
        {
            Int32 sepIndex = url.IndexOf('?');
            if (sepIndex == -1)
                return null;

            return url.Substring(sepIndex);
        }

        public static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }

        public static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
 
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
  
            if (query.Length > 0 && query[0] == '?')
            {
                query = query.Substring(1);
            }

            NameValueCollection nameValueCollection = new NameValueCollection();

            int l = (query != null) ? query.Length : 0;
            int i = 0;

            while (i < l)
            {
                // find next & while noting first = on the way (and if there are more)

                int si = i;
                int ti = -1;

                while (i < l)
                {
                    char ch = query[i];

                    if (ch == '=')
                    {
                        if (ti < 0)
                            ti = i;
                    }
                    else if (ch == '&')
                    {
                        break;
                    }

                    i++;
                }

                // extract the name / value pair

                string name = null;
                string value = null;

                if (ti >= 0)
                {
                    name = query.Substring(si, ti - si);
                    value = query.Substring(ti + 1, i - ti - 1);
                }
                else
                {
                    value = query.Substring(si, i - si);
                }

                // add name / value pair to the collection
                nameValueCollection.Add(name, value);

                // trailing '&'

                if (i == l - 1 && query[i] == '&')
                {
                    nameValueCollection.Add(null, string.Empty);
                }

                i++;
            }
            return nameValueCollection;
        }

        public static string ToString(NameValueCollection nameValueCollection)
        {
            return ToString(nameValueCollection, null);
        }

        public static string ToString(NameValueCollection nameValueCollection, IDictionary excludeKeys)
        {
            int n = nameValueCollection.Count;
            if (n == 0)
                return string.Empty;

            StringBuilder s = new StringBuilder();
            string key, keyPrefix, item;

            for (int i = 0; i < n; i++)
            {
                key = nameValueCollection.GetKey(i);

                if (excludeKeys != null && key != null && excludeKeys[key] != null)
                {
                    continue;
                }

                keyPrefix = (!key.IsNullOrEmpty()) ? (key + "=") : string.Empty;

                String[] values = nameValueCollection[i];
                int numValues = (values != null) ? values.Length : 0;

                if (s.Length > 0)
                {
                    s.Append('&');
                }

                if (numValues == 1)
                {
                    s.Append(keyPrefix);
                    item = values[0];
                    s.Append(item);
                }
                else if (numValues == 0)
                {
                    s.Append(keyPrefix);
                }
                else
                {
                    for (int j = 0; j < numValues; j++)
                    {
                        if (j > 0)
                        {
                            s.Append('&');
                        }
                        s.Append(keyPrefix);
                        item = (string)values[j];
                        s.Append(item);
                    }
                }
            }

            return s.ToString();
        }

    }
}
