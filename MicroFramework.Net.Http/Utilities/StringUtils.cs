using System;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http.Utilities
{
    /// <summary>
    /// StringUtils class.
    /// </summary>
    public static class StringUtils
    {
        private static readonly Decoder decoder = Encoding.UTF8.GetDecoder();

        /// <summary>
        /// Convert a byte array to UTF8 string.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <param name="offset">The offset in the byte array for the first character to convert.</param>
        /// <param name="count">The number of bytes starting from offset to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ByteArrayToString(byte[] bytes, int offset, int count)
        {
            if (count > 0)
            {
                int bytesUsed, charsUsed;
                bool completed;
                var chars = new char[count];
                decoder.Convert(bytes, offset, count, chars, 0, count, true, out bytesUsed, out charsUsed, out completed);
                return new string(chars);
            }
            return "";
        }
    }
}
