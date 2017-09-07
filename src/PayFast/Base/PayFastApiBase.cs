namespace PayFast.Base
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PayFast.ApiTypes;
    using PayFast.Exceptions;

    public class PayFastApiBase
    {
        #region Fields

        private readonly PayFastSettings payfastSettings;

        #endregion Fields

        #region Constructor

        public PayFastApiBase(PayFastSettings payfastSettings)
        {
            this.payfastSettings = payfastSettings ?? throw new ArgumentNullException(nameof(payfastSettings));
        }

        #endregion Constructor

        #region Properties

        protected const string BaseUrl = "https://api.payfast.co.za/subscriptions/";
        protected const string ApiVersion = "v1";
        protected const string TestMode = "?testing=true";
        protected const string PingResourceUrl = "ping";
        private const string CancelResourceUrl = "cancel";

        #endregion Properties

        #region Methods

        protected HttpClient GetClient()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("merchant-id", this.payfastSettings.MerchantId);
            httpClient.DefaultRequestHeaders.Add("version", ApiVersion);
            httpClient.DefaultRequestHeaders.Add("timestamp", DateTime.UtcNow.ToString("s"));

            return httpClient;
        }

        /// <summary>
        /// Used to check if the API is responding to requests.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<bool> Ping(string token, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{PingResourceUrl}{TestMode}" : $"{token}/{PingResourceUrl}";

                this.GenerateSignature(httpClient);

                using (var response = await httpClient.GetAsync(finalUrl))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var payload = await response.Content.ReadAsStringAsync();

                        return payload == "true";
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        /// <summary>
        /// This will cancel a subscription entirely.
        /// </summary>
        /// <param name="token">The token received from PayFast. See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return variables Documentation</a> for more information</param>
        /// <param name="testing">Pass in true to test against the sandbox. This parameter, when true appends the required '?testing=true' value to the generated query string.</param>
        /// <exception cref = "PayFast.Exceptions.ApiResponseException"> Thrown when the returned StatusCode != HttpStatusCode.OK (200)</exception>
        public async Task<AdhocCancelResult> Cancel(string token, bool testing = false)
        {
            using (var httpClient = this.GetClient())
            {
                var finalUrl = testing ? $"{token}/{CancelResourceUrl}{TestMode}" : $"{token}/{CancelResourceUrl}";

                this.GenerateSignature(httpClient);

                using (var response = await httpClient.PutAsync(finalUrl, null))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Deserialize<AdhocCancelResult>();
                    }

                    throw new ApiResponseException(httpResponseMessage: response);
                }
            }
        }

        /// <summary>
        /// This method will generate the signature for the request
        /// See <a href="https://www.payfast.co.za/documentation/api/#Merchant_Signature_Generation">PayFast API Signature Generation Documentation</a> for more information.
        /// </summary>
        protected string GenerateSignature(HttpClient httpClient, params KeyValuePair<string, string>[] parameters)
        {
            var dictionary = new SortedDictionary<string, string>();

            foreach (var header in httpClient.DefaultRequestHeaders)
            {
                dictionary.Add(key: header.Key, value: header.Value.First());
            }

            foreach (var keyValuePair in parameters)
            {
                dictionary.Add(key: keyValuePair.Key, value: keyValuePair.Value);
            }

            if (!string.IsNullOrWhiteSpace(this.payfastSettings.PassPhrase))
            {
                dictionary.Add(key: "passphrase", value: this.payfastSettings.PassPhrase);
            }

            var stringBuilder = new StringBuilder();
            var last = dictionary.Last();

            foreach (var keyValuePair in dictionary)
            {
                stringBuilder.Append($"{keyValuePair.Key.UrlEncode()}={keyValuePair.Value.UrlEncode()}");

                if (keyValuePair.Key != last.Key)
                {
                    stringBuilder.Append("&");
                }
            }

            httpClient.DefaultRequestHeaders.Add(name: "signature", value: stringBuilder.CreateHash());

            if(parameters.Length > 0)
            {
                var jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{");

                var lastParameter = parameters.Last();

                foreach (var keyValuePair in parameters)
                {
                    jsonStringBuilder.Append($"\"{keyValuePair.Key}\" : \"{keyValuePair.Value}\"");

                    if (lastParameter.Key != keyValuePair.Key)
                    {
                        jsonStringBuilder.Append(",");
                    }
                }

                jsonStringBuilder.Append("}");

                return jsonStringBuilder.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion Methods
    }
}
