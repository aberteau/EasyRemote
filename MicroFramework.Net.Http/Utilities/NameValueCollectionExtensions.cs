using System;
using Microsoft.SPOT;
using Techeasy.MicroFramework.Library;

namespace Techeasy.MicroFramework.Net.Http.Utilities
{
    public static class NameValueCollectionExtensions
    {
        public static String GetSingleValueString(this NameValueCollection nameValueCollection, string name)
        {
            String[] paramValues = nameValueCollection[name];

            if (paramValues == null || paramValues.Length == 0)
                throw new Exception("Aucune valeur fournie pour le param�tre : " + name);

            if (paramValues.Length > 1)
                throw new Exception("Seule une valeur est permise pour le param�tre : " + name);

            return paramValues[0];
        }

        public static Int32 GetSingleValueInt32(this NameValueCollection nameValueCollection, string name)
        {
            String valueStr = GetSingleValueString(nameValueCollection, name);
            try
            {
                return Int32.Parse(valueStr);
            }
            catch (Exception exception)
            {
                throw new Exception("La valeur fournie doit �tre num�rique pour le param�tre :" + name, exception);
            }
        }

        public static Boolean GetSingleValueBoolean(this NameValueCollection nameValueCollection, string name)
        {
            String valueStr = GetSingleValueString(nameValueCollection, name);
            return Parse.ParseBoolFromIntString(valueStr);
        }
    }
}
