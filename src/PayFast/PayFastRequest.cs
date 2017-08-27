namespace PayFast
{
    using System;
    using System.Text;
    using System.Collections.Specialized;

    using PayFast.Base;

    public class PayFastRequest : PayFastBase
    {
        #region Constructor

        /// <summary>
        /// This constructor is used when a passphrase is not setup or configured
        /// See <a href="https://www.payfast.co.za/documentation/confirm-page/">PayFast Confirm Page Documentation</a> for more information.
        /// </summary>
        public PayFastRequest() : this(string.Empty)
        {
        }

        /// <summary>
        /// This constructor is used when a passphrase has been configured
        /// See <a href="https://www.payfast.co.za/documentation/confirm-page/">PayFast Confirm Page Documentation</a> for more information.
        /// </summary>
        public PayFastRequest(string passPhrase) : base(passPhrase)
        {
        }

        #endregion Constructor

        #region Properties

        // Merchant Details

        /// <summary>
        /// The Merchant ID as given by the PayFast system. 
        /// Used to uniquely identify the receiving account. 
        /// This can be found on the merchant’s settings page.
        /// </summary>
        public string merchant_id { get; set; }

        /// <summary>
        /// The Merchant Key as given by the PayFast system. 
        /// Used to uniquely identify the receiving account. 
        /// This provides an extra level of certainty concerning 
        /// the correct account as both the ID and the Key must be 
        /// correct in order for the transaction to proceed. 
        /// This can be found on the merchant’s settings page.
        /// </summary>
        public string merchant_key { get; set; }

        /// <summary>
        /// The URL where the user is returned to after 
        /// payment has been successfully taken.
        /// Default: PayFast homepage
        /// </summary>
        public string return_url { get; set; }

        /// <summary>
        /// The URL where the user should be redirected should 
        /// they choose to cancel their payment while on the PayFast system.
        /// Default: PayFast homepage
        /// </summary>
        public string cancel_url { get; set; }

        /// <summary>
        /// The URL which is used by PayFast to post the Instant Transaction Notifications (ITNs) for this transaction.
        /// </summary>
        public string notify_url { get; set; }

        // Buyer Details

        /// <summary>
        /// The buyer’s first name.
        /// </summary>
        public string name_first { get; set; }

        /// <summary>
        /// The buyer’s last name.
        /// </summary>
        public string name_last { get; set; }

        /// <summary>
        /// The buyer’s email address
        /// </summary>
        public string email_address { get; set; }

        // Transaction Details

        /// <summary>
        /// Unique payment ID on the merchant’s system.
        /// </summary>
        public string m_payment_id { get; set; }

        /// <summary>
        /// The amount which the payer must pay in ZAR.
        /// </summary>
        public double amount { get; set; }

        /// <summary>
        /// The name of the item being charged for.
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// The description of the item being charged for.
        /// </summary>
        public string item_description { get; set; }

        /// <summary>
        /// Number 1 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public int? custom_int1 { get; set; }

        /// <summary>
        /// Number 2 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public int? custom_int2 { get; set; }

        /// <summary>
        /// Number 3 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public int? custom_int3 { get; set; }

        /// <summary>
        /// Number 4 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public int? custom_int4 { get; set; }

        /// <summary>
        /// Number 5 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public int? custom_int5 { get; set; }

        /// <summary>
        /// Number 1 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str1 { get; set; }

        /// <summary>
        /// Number 2 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str2 { get; set; }

        /// <summary>
        /// Number 3 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str3 { get; set; }

        /// <summary>
        /// Number 4 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str4 { get; set; }

        /// <summary>
        /// Number 5 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str5 { get; set; }

        // Transaction Options

        /// <summary>
        /// Whether to send email confirmation to the merchant of the transaction. 
        /// Email confirmation is automatically sent to the payer.
        /// </summary>
        public bool? email_confirmation { get; set; }

        /// <summary>
        /// The address to send the confirmation email to.
        /// </summary>
        public string confirmation_address { get; set; }

        // Recurring Billing Details

        /// <summary>
        /// The subscription type sets the recurring billing type 
        /// to either a  subscription or an ad hoc agreement.
        /// </summary>
        public SubscriptionType? subscription_type { get; set; }

        /// <summary>
        /// The date from which future subscription payments will be made.
        /// </summary>
        public DateTime? billing_date { get; set; }

        /// <summary>
        /// Future recurring amount for the subscription. 
        /// Defaults to the ‘amount’ value if not set.
        /// </summary>
        public double? recurring_amount { get; set; }

        /// <summary>
        /// The cycle period.
        /// </summary>
        public BillingFrequency? frequency { get; set; }

        /// <summary>
        /// The number of payments/cycles that will occur for this subscription. Set to 0 for infinity.
        /// </summary>
        public int? cycles { get; set; }

        // Security Options

        /// <summary>
        /// A security signature of the transmitted data taking 
        /// the form of an MD5 hash of the submitted variables. 
        /// The string from which the hash is created, 
        /// is the concatenation of the name value pairs of 
        /// all the non-blank variables with ‘&’ used as a separator
        /// </summary>
        public string signature => this.CreateHash(this.GetPropertyValueString());

        #endregion Properties

        #region Overrides

        /// <summary>
        /// generates the query string as per the PayFast specifications.
        /// PayFast also refers to this string as the GET string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = this.GetPropertyValueString();

            var securityHash = this.CreateHash(stringBuilder);

            if (string.IsNullOrWhiteSpace(this.passPhrase) && !stringBuilder.ToString().EndsWith("&"))
            {
                stringBuilder.Append($"&signature={this.UrlEncode(securityHash)}");
            }
            else
            {
                stringBuilder.Append($"signature={this.UrlEncode(securityHash)}");
            }

            return stringBuilder.ToString();
        }

        #endregion Overrides

        #region Private Methods

        private StringBuilder GetPropertyValueString()
        {
            var stringBuilder = new StringBuilder();
            var nameValueCollection = this.GetNameValueCollection();

            var lastEntryKey = this.DetermineLast(nameValueCollection);

            foreach (string key in nameValueCollection)
            {
                var value = nameValueCollection[key];

                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(this.passPhrase) && key == lastEntryKey)
                {
                    stringBuilder.Append($"{key}={this.UrlEncode(value)}");
                }
                else
                {
                    stringBuilder.Append($"{key}={this.UrlEncode(value)}&");
                }
            }

            return stringBuilder;
        }

        private string DetermineLast(NameValueCollection nameValueCollection)
        {
            string lastKey = nameValueCollection.GetKey(nameValueCollection.Count - 1);

            foreach (string key in nameValueCollection)
            {
                var value = nameValueCollection[key];

                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }
                else
                {
                    lastKey = key;
                }
            }

            return lastKey;
        }

        #endregion Private Methods
    }
}
