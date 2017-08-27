namespace System.Net.Http
{
    using Newtonsoft.Json;

    public static class HttpResponseExtensions
    {
        public static T Deserialize<T>(this HttpResponseMessage response) where T : class
        {
            var settings = new JsonSerializerSettings();
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            var stream = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(stream, settings);
        }
    }
}
