namespace PayFast
{
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PayFast.Base;
    using PayFast.ApiTypes;

    /// <summary>
    /// This class is intended to me used for adhoc payments
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

                    return null;
                }
            }
        }

        public async Task<AdhocResult> Charge(string token, int amount, string item_name, bool testing = false)
        {
            return await this.Charge(token: token, amount: amount, item_name: item_name, item_description: string.Empty, testing: testing);
        }

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

                    return null;
                }
            }
        }

        #endregion Methods
    }
}
