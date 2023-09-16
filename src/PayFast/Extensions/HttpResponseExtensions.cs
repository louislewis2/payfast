namespace System.Net.Http
{
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public static class HttpResponseExtensions
    {
        public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            var settings = new JsonSerializerSettings();
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            var stream = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stream, settings);
        }
    }
}
