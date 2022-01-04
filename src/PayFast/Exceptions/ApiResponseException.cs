namespace PayFast.Exceptions
{
    using System;
    using System.Net.Http;

    public class ApiResponseException : Exception
    {
        #region Constructor

        public ApiResponseException(HttpResponseMessage httpResponseMessage): base(message: "Invalid Response. See The HttpResponseMessage Property For Details")
        {
            this.HttpResponseMessage = httpResponseMessage;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The response from the api call
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        #endregion Properties
    }
}
