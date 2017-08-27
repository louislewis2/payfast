namespace PayFast
{
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using PayFast.Base;

    public class PayFastNotify : PayFastBase
    {
        #region Fields

        private readonly Dictionary<string, string> properties;

        #endregion Fields

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
            this.properties = new Dictionary<string, string>();
        }

        #endregion Constructor

        #region Properties

        // Transaction details

        /// <summary>
        /// Unique payment ID on the merchant’s system.
        /// </summary>
        public string m_payment_id
        {
            get
            {
                return this.properties.ValueAs(nameof(m_payment_id));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(m_payment_id), value);
            }
        }

        /// <summary>
        /// Unique transaction ID on PayFast.
        /// </summary>
        public string pf_payment_id
        {
            get
            {
                return this.properties.ValueAs(nameof(pf_payment_id));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(pf_payment_id), value);
            }
        }

        /// <summary>
        /// The status of the payment.
        /// </summary>
        public string payment_status
        {
            get
            {
                return this.properties.ValueAs(nameof(payment_status));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(payment_status), value);
            }
        }

        /// <summary>
        /// The name of the item being charged for.
        /// </summary>
        public string item_name
        {
            get
            {
                return this.properties.ValueAs(nameof(item_name));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(item_name), value);
            }
        }

        /// <summary>
        /// The description of the item being charged for.
        /// </summary>
        public string item_description
        {
            get
            {
                return this.properties.ValueAs(nameof(item_description));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(item_description), value);
            }
        }

        /// <summary>
        /// The total amount which the payer paid.
        /// </summary>
        public string amount_gross
        {
            get
            {
                return this.properties.ValueAs(nameof(amount_gross));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(amount_gross), value);
            }
        }

        /// <summary>
        /// The total in fees which was deducated from the amount.
        /// </summary>
        public string amount_fee
        {
            get
            {
                return this.properties.ValueAs(nameof(amount_fee));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(amount_fee), value);
            }
        }

        /// <summary>
        /// The net amount credited to the receiver’s account.
        /// </summary>
        public string amount_net
        {
            get
            {
                return this.properties.ValueAs(nameof(amount_net));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(amount_net), value);
            }
        }

        /// <summary>
        /// Number 1 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str1
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_str1));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_str1), value);
            }
        }

        /// <summary>
        /// Number 2 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str2
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_str2));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_str2), value);
            }
        }

        /// <summary>
        /// Number 3 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str3
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_str3));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_str3), value);
            }
        }

        /// <summary>
        /// Number 4 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str4
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_str4));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_str4), value);
            }
        }

        /// <summary>
        /// Number 5 in a series of 5 custom string variables (custom_str1, custom_str2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_str5
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_str5));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_str5), value);
            }
        }

        /// <summary>
        /// Number 1 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int1
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_int1));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_int1), value);
            }
        }

        /// <summary>
        /// Number 2 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int2
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_int2));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_int2), value);
            }
        }

        /// <summary>
        /// Number 3 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int3
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_int3));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_int3), value);
            }
        }

        /// <summary>
        /// Number 4 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int4
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_int4));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_int4), value);
            }
        }

        /// <summary>
        /// Number 5 in a series of 5 custom integer variables (custom_int1, custom_int2…) 
        /// which can be used by the merchant as pass-through variables. 
        /// They will be posted back to the merchant at the completion of the transaction.
        /// </summary>
        public string custom_int5
        {
            get
            {
                return this.properties.ValueAs(nameof(custom_int5));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(custom_int5), value);
            }
        }

        // Buyer details

        /// <summary>
        /// The buyer’s first name.
        /// </summary>
        public string name_first
        {
            get
            {
                return this.properties.ValueAs(nameof(name_first));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(name_first), value);
            }
        }

        /// <summary>
        /// The buyer’s last name.
        /// </summary>
        public string name_last
        {
            get
            {
                return this.properties.ValueAs(nameof(name_last));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(name_last), value);
            }
        }

        /// <summary>
        /// The buyer’s email address
        /// </summary>
        public string email_address
        {
            get
            {
                return this.properties.ValueAs(nameof(email_address));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(email_address), value);
            }
        }

        // Merchant details

        /// <summary>
        /// The Merchant ID as given by the PayFast system. 
        /// Used to uniquely identify the receiver’s account.
        /// </summary>
        public string merchant_id
        {
            get
            {
                return this.properties.ValueAs(nameof(merchant_id));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(merchant_id), value);
            }
        }

        // Recurring billing details

        /// <summary>
        /// Unique ID on PayFast that represents the subscription
        /// </summary>
        public string token
        {
            get
            {
                return this.properties.ValueAs(nameof(token));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(token), value);
            }
        }

        /// <summary>
        /// The date from which future subscription payments will be made.
        /// </summary>
        public string billing_date
        {
            get
            {
                return this.properties.ValueAs(nameof(billing_date));
            }
            set
            {
                this.properties.AddOrUpdate(nameof(billing_date), value);
            }
        }

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

        public void FromFormCollection(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection == null || nameValueCollection.Count() < 1)
            {
                return;
            }

            foreach (KeyValuePair<string, string> keyValuePair in nameValueCollection)
            {
                if (keyValuePair.Key == nameof(this.signature))
                {
                    this.signature = keyValuePair.Value;

                    continue;
                }

                this.properties.AddOrUpdate(key: keyValuePair.Key, value: keyValuePair.Value);
            }
        }

        public string GetCalculatedSignature()
        {
            var stringBuilder = new StringBuilder();

            var lastEntryKey = this.properties.Last();

            foreach (var property in this.properties)
            {
                if (property.Key == nameof(this.signature))
                {
                    continue;
                }

                var value = property.Value;

                if (property.Key == nameof(this.billing_date) && string.IsNullOrEmpty(property.Value))
                {
                    continue;
                }
                if (property.Key == nameof(this.token) && string.IsNullOrEmpty(property.Value))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(passPhrase) && property.Key == lastEntryKey.Key)
                {
                    stringBuilder.Append($"{property.Key}={this.UrlEncode(property.Value)}");
                }
                else
                {
                    stringBuilder.Append($"{property.Key}={this.UrlEncode(property.Value)}&");
                }
            }

            return this.CreateHash(stringBuilder);
        }

        public Dictionary<string, string> GetUnderlyingProperties()
        {
            return this.properties;
        }

        public bool CheckSignature()
        {
            var securityHash = this.GetCalculatedSignature();

            return this.signature == securityHash;
        }

        private string DetermineLast(NameValueCollection nameValueCollection)
        {
            string lastKey = nameValueCollection.GetKey(nameValueCollection.Count - 2);

            foreach (string key in nameValueCollection)
            {
                if (key == nameof(this.signature))
                {
                    continue;
                }

                var value = nameValueCollection[key];

                if (key == nameof(this.billing_date) && string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (key == nameof(this.token) && string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                lastKey = key;
            }

            return lastKey;
        }

        #endregion Methods
    }
}
