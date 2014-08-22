using System;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Utilities
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this String key)
        {
            return key == null || key.Equals(String.Empty);
        }

        public static bool IsNullOrWhiteSpace(this String key)
        {
            return IsNullOrEmpty(key) || (key.Trim().Equals(String.Empty));
        }

        public static bool StartsWith(this String input, String part)
        {
            if (input.Length < part.Length)
                return false;

            for (int i = 0; i < part.Length; i++)
                if (input[i] != part[i])
                    return false;

            return true;
        }

        public static String Replace(this String input, String oldValue, String newValue)
        {
            var stringBuilder = new StringBuilder(input);
            var stringBuilderDest = stringBuilder.Replace(oldValue, newValue);
            return stringBuilderDest.ToString();
        }
    }
}
