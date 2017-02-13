namespace PayFast
{
    using System.Text;

    public class PayFastNotify : PayFastBase
    {
        #region Constructor

        /// <summary>
        /// This constructor does not allow for a argument
        /// because it is intended to be called by the mvc model binder.
        /// If a passphrase is being used, make a call to SetPassPhrase(string passPhrase)
        /// on this class after it has been passed in by the model binder
        /// See <a href="https://www.payfast.co.za/documentation/return-variables/">PayFast Return Variable Page Documentation</a> for more information.
        /// See <a href="https://www.payfast.co.za/documentation/itn/">PayFast ITN Page Documentation</a> for more information.
        /// </summary>
        public PayFastNotify() : base(string.Empty)
        {
        }

        #endregion Constructor

        #region Properties

        // Transaction details

        /// <summary>
        /// Unique payment ID on the merchant’s system.
        /// </summary>
        public string m_payment_id { get; set; }

        /// <summary>
        /// Unique transaction ID on PayFast.
        /// </summary>
        public string pf_payment_id { get; set; }

        /// <summary>
        /// The status of the payment.
        /// </summary>
        public string payment_status { get; set; }

        /// <summary>
        /// The name of the item being charged for.
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// The description of the item being charged for.
        /// </summary>
        public string item_description { get; set; }

        /// <summary>
        /// The total amount which the payer paid.
        /// </summary>
        public string amount_gross { get; set; }

        /// <summary>
        /// The total in fees which was deducated from the amount.
        /// </summary>
        public string amount_fee { get; set; }

        /// <summary>
        /// The net amount credited to the receiver’s account.
        /// </summary>
        public string amount_net { get; set; }

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

        /// <summary>
        /// Number 1 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int1 { get; set; }

        /// <summary>
        /// Number 2 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int2 { get; set; }

        /// <summary>
        /// Number 3 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int3 { get; set; }

        /// <summary>
        /// Number 4 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int4 { get; set; }

        /// <summary>
        /// Number 5 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int5 { get; set; }

        // Buyer details

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

        // Merchant details

        /// <summary>
        /// The Merchant ID as given by the PayFast system. 
        /// Used to uniquely identify the receiver’s account.
        /// </summary>
        public string merchant_id { get; set; }

        // Recurring billing details

        /// <summary>
        /// Unique ID on PayFast that represents the subscription
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// The date from which future subscription payments will be made.
        /// </summary>
        public string billing_date { get; set; }

        // Security information

        /// <summary>
        /// A security signature of the transmitted data taking 
        /// the form of an MD5 hash of the submitted variables. 
        /// The string from which the hash is created, 
        /// is the concatenation of the name value pairs of 
        /// all the variables with ‘&’ used as a separator.
        /// </summary>
        public string signature { get; set; }

        #endregion #region Properties

        #region Methods

        public string GetCalculatedSignature()
        {
            var stringBuilder = new StringBuilder();
            var nameValueCollection = this.GetNameValueCollection();

            var lastEntryKey = nameValueCollection.GetKey(nameValueCollection.Count - 2);

            foreach (string key in nameValueCollection)
            {
                if (key == nameof(this.signature))
                {
                    continue;
                }

                var value = nameValueCollection[key];

                if (string.IsNullOrWhiteSpace(passPhrase) && key == lastEntryKey)
                {
                    stringBuilder.Append($"{key}={this.UrlEncode(value)}");
                }
                else
                {
                    stringBuilder.Append($"{key}={this.UrlEncode(value)}&");
                }
            }

            return this.CreateHash(stringBuilder);
        }

        public bool CheckSignature()
        {
            var securityHash = this.GetCalculatedSignature();

            return this.signature == securityHash;
        }

        #endregion Methods
    }
}
