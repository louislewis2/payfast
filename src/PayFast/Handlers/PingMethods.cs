namespace PayFast
{
    using System.Threading.Tasks;

    public static class PingMethods
    {
        #region Fields

        private const string requestUri = "ping";

        #endregion Fields

        #region Methods

        public static async Task<string> Ping(this PayFastIntegrationClient client, bool testing = false)
        {
            return await client.Get<string>(requestUri: requestUri, testing: testing);
        }

        #endregion Methods
    }
}
