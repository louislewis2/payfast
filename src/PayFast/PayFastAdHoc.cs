namespace PayFast
{
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PayFast.Base;
    using PayFast.ApiTypes;
    using PayFast.Exceptions;

    /// <summary>
    /// This class is intended to be used for adhoc payments
    /// See <a href="https://www.payfast.co.za/documentation/ad-hoc-payments-api-endpoints/">PayFast Ad-Hoc Payments Documentation</a> for more information.
    /// </summary>
    public class PayFastAdHoc : PayFastApiBase
    {
        #region Fields

        private const string chargeResourceUrl = "adhoc";
        private const string fetchResourceUrl = "fetch";

        #endregion Fields

        #region Constructor

        public PayFastAdHoc(PayFastSettings payfastSettings) : base(payfastSettings: payfastSettings)
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
        public async Task<AdhocFetchResult> Fetch(string token, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{fetchResourceUrl}{TestMode}" : $"{token}/{fetchResourceUrl}";

                this.GenerateSignature(httpClient);

                using (var response = await httpClient.GetAsync(finalUrl))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<AdhocFetchResult>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        /// <summary>
        /// Charge an ad hoc subscription based on token.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="amount">Future recurring amount for the subscription. In ZAR and amount in cents and not X.XX</param>
        /// <param name="item_name">The name of the item being charged for. (100 chars)</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<AdhocResult> Charge(string token, int amount, string item_name, bool testing = false)
        {
            return await this.Charge(token: token, amount: amount, item_name: item_name, item_description: string.Empty, testing: testing);
        }

        /// <summary>
        /// Charge an ad hoc subscription based on token.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="amount">Future recurring amount for the subscription. In ZAR and amount in cents and not X.XX</param>
        /// <param name="item_name">The name of the item being charged for. (100 chars)</param>
        /// <param name="item_description">The description of the item being charged for.  (255 chars)</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<AdhocResult> Charge(string token, int amount, string item_name, string item_description, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{chargeResourceUrl}{TestMode}" : $"{token}/{chargeResourceUrl}";

                var incommingParameters = new List<KeyValuePair<string, string>>();
                incommingParameters.Add(new KeyValuePair<string, string>("amount", amount.ToString()));
                incommingParameters.Add(new KeyValuePair<string, string>("item_name", item_name));

                if (!string.IsNullOrWhiteSpace(item_description))
                {
                    incommingParameters.Add(new KeyValuePair<string, string>("item_description", item_description));
                }

                var parameterValue = this.GenerateSignature(httpClient, incommingParameters.ToArray());
                var content = new StringContent(parameterValue, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(finalUrl, content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<AdhocResult>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        #endregion Methods
    }
}
