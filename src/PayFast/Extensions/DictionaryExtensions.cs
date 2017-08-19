namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate(this Dictionary<string, string> dictionary, string key, string value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary cannot be null");
            }

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key: key, value: string.IsNullOrEmpty(value) ? string.Empty : value);
            }
            else
            {
                dictionary[key] = value;
            }
        }

        public static string ValueAs(this Dictionary<string, string> dictionary, string key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary cannot be null");
            }

            if (!dictionary.ContainsKey(key))
            {
                return null;
            }
            else
            {
                return dictionary[key];
            }
        }
    }
}
