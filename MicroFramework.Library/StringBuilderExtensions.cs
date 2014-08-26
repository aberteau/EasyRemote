using System;
using System.Text;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Library
{
    public static class StringBuilderExtensions
    {
        public static bool IsEmpty(this StringBuilder stringBuilder)
        {
            return stringBuilder.Length == 0;
        }
    }
}
