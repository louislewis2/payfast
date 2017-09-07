namespace PayFast
{
    using System;
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PayFast.Base;
    using PayFast.ApiTypes;
    using PayFast.Exceptions;

    /// <summary>
    /// This class is intended to be used for subscriptions
    /// See <a href="https://www.payfast.co.za/documentation/subscription-payments-api-endpoints/">PayFast Subscription Documentation</a> for more information.
    /// </summary>
    public class PayFastSubscription : PayFastApiBase
    {
        #region Fields

        private const string pauseResourceUrl = "pause";
        private const string unpauseResourceUrl = "unpause";
        private const string updateResourceUrl = "update";
        private const string fetchResourceUrl = "fetch";

        #endregion Fields

        #region Constructor

        public PayFastSubscription(PayFastSettings payfastSettings) : base(payfastSettings: payfastSettings)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns a JSON object containing the subscription details.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<UpdateResponse> Fetch(string token, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{fetchResourceUrl}{TestMode}" : $"{token}/{fetchResourceUrl}";

                this.GenerateSignature(httpClient);

                using (var response = await httpClient.GetAsync(finalUrl))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<UpdateResponse>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        /// <summary>
        /// 'Freeze' a subscription, for a duration of 1 cycle
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<AdhocResult> Pause(string token, bool testing = false)
        {
            return await this.Pause(token: token, cycles: 1, testing: testing);
        }

        /// <summary>
        /// 'Freeze' a subscription, for a duration indicated by the 'cycles' variable.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <param name="cycles">The number of payments/cycles that will occur for this subscription. Set to 0 for infinity.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<AdhocResult> Pause(string token, int cycles, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{pauseResourceUrl}{TestMode}" : $"{token}/{pauseResourceUrl}";

                var incommingParameters = new List<KeyValuePair<string, string>>();
                incommingParameters.Add(new KeyValuePair<string, string>("cycles", cycles.ToString()));

                var parameterValue = this.GenerateSignature(httpClient, incommingParameters.ToArray());
                var content = new StringContent(parameterValue, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(finalUrl, content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<AdhocResult>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        /// <summary>
        /// 'Unfreeze' a subscription.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<AdhocResult> UnPause(string token, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{unpauseResourceUrl}{TestMode}" : $"{token}/{unpauseResourceUrl}";

                this.GenerateSignature(httpClient);

                using (var response = await httpClient.PutAsync(finalUrl, null))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<AdhocResult>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
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
        public async Task<UpdateResponse> Update(string token, int? cycles = null, BillingFrequency? frequency = null, DateTime? run_date = null, int? amount = null, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{updateResourceUrl}{TestMode}" : $"{token}/{updateResourceUrl}";

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

                var parameterValue = this.GenerateSignature(httpClient, incommingParameters.ToArray());
                var content = new StringContent(parameterValue, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PatchAsync(finalUrl, content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<UpdateResponse>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        #endregion Methods
    }
}
