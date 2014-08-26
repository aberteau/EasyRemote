using System;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Library
{
    public class Parse
    {
        public static bool ParseBool(Int32 intValue)
        {
            if(intValue > 1 || intValue < 0)
                throw new Exception("0 ou 1 attendu");

            return (intValue == 1);
        }

        public static bool ParseBoolFromIntString(string intValueStr)
        {
            Int32 intValue = 0;
            try
            {
                intValue = Int32.Parse(intValueStr);
            }
            catch (Exception exception)
            {
                throw new Exception("La valeur fournie doit être numérique pour le paramètre :", exception);
            }
            return ParseBool(intValue);
        }
    }
}
