namespace System.Text
{
    using System.Security.Cryptography;

    public static class StringBuilderExtensions
    {
        public static string CreateHash(this StringBuilder input)
        {
            var inputStringBuilder = new StringBuilder(input.ToString());

            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(inputStringBuilder.ToString());

            var hash = md5.ComputeHash(inputBytes);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
