﻿namespace Game.Shared.Utility
{
    public static  class StringTools
    {
        public static string RemoveLineEndings(this string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return value;
            }
            var lineSeparator = ((char) 0x2028).ToString();
            var paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace(lineSeparator, string.Empty)
                .Replace(paragraphSeparator, string.Empty);
        }
    }
}