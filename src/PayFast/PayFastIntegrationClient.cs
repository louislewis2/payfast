namespace PayFast
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;

    using PayFast.Exceptions;

    public class PayFastIntegrationClient
    {
        #region Fields

        private const string ApiVersion = "v1";
        private const string TestMode = "?testing=true";

        private readonly HttpClient httpClient;
        private readonly PayFastSettings payFastSettings;

        #endregion Fields

        #region Constructor

        public PayFastIntegrationClient(HttpClient httpClient, IOptions<PayFastSettings> payFastSettingsOptions)
        {
            this.httpClient = httpClient;
            this.payFastSettings = payFastSettingsOptions.Value;

        }

        #endregion Constructor

        #region Methods

        public async Task<T> Get<T>(string requestUri, bool testing = false)
        {
            var finalRequestUri = testing ? $"{requestUri}{TestMode}" : requestUri;
            var httpRequestMessage = new HttpRequestMessage(method: HttpMethod.Get, requestUri: finalRequestUri);

            this.HandleHeaders(httpRequestMessage: httpRequestMessage);
            this.GenerateSignature(httpRequestMessage: httpRequestMessage, incommingParameters: null);

            return await this.DoRequest<T>(httpRequestMessage: httpRequestMessage);
        }

        public async Task<T> Post<T>(string requestUri, KeyValuePair<string, string>[] incommingParameters, bool testing = false)
        {
            var finalRequestUri = testing ? $"{requestUri}{TestMode}" : requestUri;
            var httpRequestMessage = new HttpRequestMessage(method: HttpMethod.Post, requestUri: finalRequestUri);

            if (incommingParameters != null && incommingParameters.Length > 0)
            {
                this.HandleHeaders(httpRequestMessage: httpRequestMessage);
                var signatureValue = this.GenerateSignature(httpRequestMessage: httpRequestMessage, incommingParameters: incommingParameters);

                var stringContent = new StringContent(signatureValue, Encoding.UTF8, "application/json");

                httpRequestMessage.Content = stringContent;
            }

            return await this.DoRequest<T>(httpRequestMessage: httpRequestMessage);
        }

        public async Task<T> Patch<T>(string requestUri, KeyValuePair<string, string>[] incommingParameters, bool testing = false)
        {
            var finalRequestUri = testing ? $"{requestUri}{TestMode}" : requestUri;
            var httpRequestMessage = new HttpRequestMessage(method: HttpMethod.Patch, requestUri: finalRequestUri);

            if (incommingParameters != null && incommingParameters.Length > 0)
            {
                this.HandleHeaders(httpRequestMessage: httpRequestMessage);
                var signatureValue = this.GenerateSignature(httpRequestMessage: httpRequestMessage, incommingParameters: incommingParameters);

                var stringContent = new StringContent(signatureValue, Encoding.UTF8, "application/json");

                httpRequestMessage.Content = stringContent;
            }

            return await this.DoRequest<T>(httpRequestMessage: httpRequestMessage);
        }

        public async Task<T> Put<T>(string requestUri, KeyValuePair<string, string>[] incommingParameters, bool testing = false)
        {
            var finalRequestUri = testing ? $"{requestUri}{TestMode}" : requestUri;
            var httpRequestMessage = new HttpRequestMessage(method: HttpMethod.Put, requestUri: finalRequestUri);

            this.HandleHeaders(httpRequestMessage: httpRequestMessage);
            var signatureValue = this.GenerateSignature(httpRequestMessage: httpRequestMessage, incommingParameters: incommingParameters);

            if (!string.IsNullOrEmpty(signatureValue))
            {
                var stringContent = new StringContent(signatureValue, Encoding.UTF8, "application/json");

                httpRequestMessage.Content = stringContent;
            }

            return await this.DoRequest<T>(httpRequestMessage: httpRequestMessage);
        }

        #endregion Methods

        #region Private Methods

        private async Task<T> DoRequest<T>(HttpRequestMessage httpRequestMessage)
        {
            var httpReponseMessage = await this.httpClient.SendAsync(request: httpRequestMessage);

            if (httpReponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return await httpReponseMessage.Deserialize<T>();
            }

            throw new ApiResponseException(httpResponseMessage: httpReponseMessage);
        }

        private void HandleHeaders(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.Add(name: "merchant-id", value: this.payFastSettings.MerchantId);
            httpRequestMessage.Headers.Add(name: "version", value: ApiVersion);
            httpRequestMessage.Headers.Add(name: "timestamp", value: DateTime.UtcNow.ToString("s"));
        }

        /// <summary>
        /// This method will generate the signature for the request
        /// See <a href="https://www.payfast.co.za/documentation/api/#Merchant_Signature_Generation">PayFast API Signature Generation Documentation</a> for more information.
        /// </summary>
        private string GenerateSignature(HttpRequestMessage httpRequestMessage, KeyValuePair<string, string>[] incommingParameters)
        {
            var dictionary = new SortedDictionary<string, string>();

            foreach (var header in httpRequestMessage.Headers)
            {
                dictionary.Add(key: header.Key, value: header.Value.First());
            }

            if (incommingParameters != null)
            {
                foreach (var keyValuePair in incommingParameters)
                {
                    dictionary.Add(key: keyValuePair.Key, value: keyValuePair.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(this.payFastSettings.PassPhrase))
            {
                dictionary.Add(key: "passphrase", value: this.payFastSettings.PassPhrase);
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

            httpRequestMessage.Headers.Add(name: "signature", value: stringBuilder.CreateHash());

            if (incommingParameters != null && incommingParameters.Length > 0)
            {
                var jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{");

                var lastParameter = incommingParameters.Last();

                foreach (var keyValuePair in incommingParameters)
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


        #endregion Private Methods
    }
}
