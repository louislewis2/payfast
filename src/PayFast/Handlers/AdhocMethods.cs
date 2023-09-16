namespace PayFast
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PayFast.ApiTypes;

    public static class AdhocMethods
    {
        #region Fields

        private const string subscriptionRequestBaseUri = "subscriptions/";
        private const string chargeResourceUrl = "/adhoc";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Charge an ad hoc subscription based on token.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="amount">Future recurring amount for the subscription. In ZAR and amount in cents and not X.XX</param>
        /// <param name="item_name">The name of the item being charged for. (100 chars)</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<AdhocResult> Charge(this PayFastIntegrationClient payFastIntegrationClient, string token, int amount, string item_name, bool testing = false)
        {
            return await payFastIntegrationClient.Charge(token: token, amount: amount, item_name: item_name, item_description: string.Empty, m_payment_id: string.Empty, testing: testing);
        }

        /// <summary>
        /// Charge an ad hoc subscription based on token.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="amount">Future recurring amount for the subscription. In ZAR and amount in cents and not X.XX</param>
        /// <param name="item_name">The name of the item being charged for. (100 chars)</param>
        /// <param name="item_description">The description of the item being charged for.  (255 chars)</param>
        /// <param name="m_payment_id">Unique payment ID on the merchant’s system. (100 chars)</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<AdhocResult> Charge(this PayFastIntegrationClient payFastIntegrationClient, string token, int amount, string item_name, string item_description, string m_payment_id, bool testing = false)
        {
            var incommingParameters = new List<KeyValuePair<string, string>>();
            incommingParameters.Add(new KeyValuePair<string, string>("amount", amount.ToString()));
            incommingParameters.Add(new KeyValuePair<string, string>("item_name", item_name));

            if (!string.IsNullOrWhiteSpace(item_description))
            {
                incommingParameters.Add(new KeyValuePair<string, string>("item_description", item_description));
            }

            if (!string.IsNullOrWhiteSpace(m_payment_id))
            {
                incommingParameters.Add(new KeyValuePair<string, string>("m_payment_id", m_payment_id));
            }

            return await payFastIntegrationClient.Post<AdhocResult>(
                requestUri: $"{subscriptionRequestBaseUri}{token}{chargeResourceUrl}",
                incommingParameters: incommingParameters.ToArray(),
                testing: testing);
        }

        #endregion Methods
    }
}
