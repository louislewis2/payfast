namespace System
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string UrlEncode(this string url)
        {
            string encoded = System.Net.WebUtility.UrlEncode(url.Trim());

            //Fix for .NET missing out some characaters when encoding
            StringBuilder sb = new StringBuilder(encoded);
            return sb
                  .Replace("(", "%28")
                  .Replace(")", "%29")
                  .Replace("!", "%21")
                  .ToString();
        }
    }
}
