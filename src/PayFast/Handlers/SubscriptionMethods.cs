namespace PayFast
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PayFast.ApiTypes;

    public static class SubscriptionMethods
    {
        #region Fields

        private const string subscriptionRequestBaseUri = "subscriptions/";
        private const string pauseResourceUrl = "/pause";
        private const string unpauseResourceUrl = "/unpause";
        private const string updateResourceUrl = "/update";
        private const string fetchResourceUrl = "/fetch";
        private const string cancelResourceUrl = "/cancel";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Returns a JSON object containing the subscription details.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<UpdateResponse> Fetch(this PayFastIntegrationClient payFastIntegrationClient, string token, bool testing = false)
        {
            return await payFastIntegrationClient.Get<UpdateResponse>(
                requestUri: $"{subscriptionRequestBaseUri}{token}{fetchResourceUrl}", 
                testing: testing);
        }

        /// <summary>
        /// This allows for multiple subscription values to be updated. If you do not wish to change a value, set the parameter to null, or do not pass in a value as it will default to null
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="cycles">The number of payments/cycles that will occur for this subscription. Set to 0 for infinity.</param>
        /// <param name="frequency">The cycle period.</param>
        /// <param name="run_date">The date from which the cycle will be calculated</param>
        /// <param name="amount">Future recurring amount for the subscription. In ZAR and amount in cents and not X.XX</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<UpdateResponse> Update(this PayFastIntegrationClient payFastIntegrationClient, string token, int? cycles = null, BillingFrequency? frequency = null, DateTime? run_date = null, int? amount = null, bool testing = false)
        {
            var incommingParameters = new List<KeyValuePair<string, string>>();

            if (cycles.HasValue)
            {
                incommingParameters.Add(new KeyValuePair<string, string>("cycles", cycles.Value.ToString()));
            }

            if (frequency.HasValue)
            {
                incommingParameters.Add(new KeyValuePair<string, string>("frequency", ((int)frequency.Value).ToString()));
            }

            if (run_date.HasValue)
            {
                incommingParameters.Add(new KeyValuePair<string, string>("run_date", run_date.Value.ToPayFastDate()));
            }

            if (amount.HasValue)
            {
                incommingParameters.Add(new KeyValuePair<string, string>("amount", amount.Value.ToString()));
            }

            if (incommingParameters.Count < 1)
            {
                throw new ArgumentException("At least one parameter must be supplied");
            }

            return await payFastIntegrationClient.Patch<UpdateResponse>(
                requestUri: $"{subscriptionRequestBaseUri}{token}{updateResourceUrl}",
                incommingParameters: incommingParameters.ToArray(),
                testing: testing);
        }

        /// <summary>
        /// 'Freeze' a subscription, for a duration of 1 cycle
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<AdhocResult> Pause(this PayFastIntegrationClient payFastIntegrationClient, string token, bool testing = false)
        {
            return await payFastIntegrationClient.Pause(token: token, cycles: 1, testing: testing);
        }

        /// <summary>
        /// 'Freeze' a subscription, for a duration indicated by the 'cycles' variable.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <param name="cycles">The number of payments/cycles that will occur for this subscription. Set to 0 for infinity.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<AdhocResult> Pause(this PayFastIntegrationClient payFastIntegrationClient, string token, int cycles, bool testing = false)
        {
            var incommingParameters = new List<KeyValuePair<string, string>>();
            incommingParameters.Add(new KeyValuePair<string, string>("cycles", cycles.ToString()));

            return await payFastIntegrationClient.Put<AdhocResult>(
                requestUri: $"{subscriptionRequestBaseUri}{token}{pauseResourceUrl}",
                incommingParameters: incommingParameters.ToArray(),
                testing: testing);
        }

        /// <summary>
        /// 'Unfreeze' a subscription.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<AdhocResult> UnPause(this PayFastIntegrationClient payFastIntegrationClient, string token, bool testing = false)
        {
            return await payFastIntegrationClient.Put<AdhocResult>(
                requestUri: $"{subscriptionRequestBaseUri}{token}{unpauseResourceUrl}",
                incommingParameters: null,
                testing: testing);
        }

        /// <summary>
        /// This will cancel a subscription entirely.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public static async Task<AdhocCancelResult> Cancel(this PayFastIntegrationClient payFastIntegrationClient, string token, bool testing = false)
        {
            return await payFastIntegrationClient.Put<AdhocCancelResult>(
                requestUri: $"{subscriptionRequestBaseUri}{token}{cancelResourceUrl}",
                incommingParameters: null,
                testing: testing);
        }

        #endregion Methods
    }
}
