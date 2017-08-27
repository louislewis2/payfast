namespace PayFast.Base
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;

    public class PayFastBase
    {
        #region Fields

        protected string passPhrase;

        #endregion Fields

        #region Constructor

        public PayFastBase(string passPhrase)
        {
            this.passPhrase = passPhrase;
        }

        #endregion Constructor

        #region Methods

        public void SetPassPhrase(string passPhrase)
        {
            this.passPhrase = passPhrase;
        }

        public NameValueCollection GetNameValueCollection()
        {
            var collection = new NameValueCollection();

            this.GetType().GetProperties()
                .Where(x => x.Name != "signature")
                .ToList()
                .ForEach(x => collection.Add(x.Name, this.GetPropertyValue(x)));

            return collection;
        }

        private string GetPropertyValue(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(BillingFrequency)))
            {
                var currentEnumValue = propertyInfo.GetValue(this, null);

                if (currentEnumValue == null)
                {
                    return string.Empty;
                }

                var billingFrequency = currentEnumValue.ToEnum<BillingFrequency>();

                return ((byte)billingFrequency).ToString();
            }

            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(SubscriptionType)))
            {
                var currentEnumValue = propertyInfo.GetValue(this, null);

                if(currentEnumValue == null)
                {
                    return string.Empty;
                }

                var subscriptionType = currentEnumValue.ToEnum<SubscriptionType>();

                return ((byte)subscriptionType).ToString();
            }

            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(bool)))
            {
                if (propertyInfo.PropertyType == typeof(bool?))
                {
                    var booleanValue = (bool?)propertyInfo.GetValue(this, null);

                    return booleanValue.HasValue ?  booleanValue.Value ? "1" : "0" : string.Empty;
                }
                else
                {
                    var booleanValue = (bool)propertyInfo.GetValue(this, null);

                    return booleanValue ? "1" : "0";
                }
            }

            if (propertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime)))
            {
                if(propertyInfo.PropertyType == typeof(DateTime?))
                {
                    var dateTimeValue = (DateTime?)propertyInfo.GetValue(this, null);

                    return dateTimeValue.ToPayFastDate();
                }
                else
                {
                    var dateTimeValue = (DateTime)propertyInfo.GetValue(this, null);

                    return dateTimeValue.ToPayFastDate();
                }
            }

            return propertyInfo.GetValue(this, null) == null ? string.Empty : propertyInfo.GetValue(this, null).ToString();
        }

        protected string UrlEncode(string url)
        {
            Dictionary<string, string> convertPairs = new Dictionary<string, string>() { { "%", "%25" }, { "!", "%21" }, { "#", "%23" }, { " ", "+" },
            { "$", "%24" }, { "&", "%26" }, { "'", "%27" }, { "(", "%28" }, { ")", "%29" }, { "*", "%2A" }, { "+", "%2B" }, { ",", "%2C" },
            { "/", "%2F" }, { ":", "%3A" }, { ";", "%3B" }, { "=", "%3D" }, { "?", "%3F" }, { "@", "%40" }, { "[", "%5B" }, { "]", "%5D" } };

            var replaceRegex = new Regex(@"[%!# $&'()*+,/:;=?@\[\]]");
            MatchEvaluator matchEval = match => convertPairs[match.Value];
            string encoded = replaceRegex.Replace(url, matchEval);

            return encoded;
        }

        protected string CreateHash(StringBuilder input)
        {
            var inputStringBuilder = new StringBuilder(input.ToString());
            if (!string.IsNullOrWhiteSpace(passPhrase))
            {
                inputStringBuilder.Append($"passphrase={this.UrlEncode(this.passPhrase)}");
            }

            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(inputStringBuilder.ToString());

            var hash = md5.ComputeHash(inputBytes);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        #endregion Methods
    }
}
