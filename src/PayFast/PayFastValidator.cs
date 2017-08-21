namespace PayFast
{
    using System;
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class PayFastValidator
    {
        #region Constructor

        public PayFastValidator(PayFastSettings settings, PayFastNotify notify, IPAddress requestIpAddress)
        {
            this.Settings = settings;
            this.Notify = notify;
            this.RequestIpAddress = requestIpAddress;
        }

        #endregion Constructor

        #region Properties

        public PayFastSettings Settings { get; private set; }
        public PayFastNotify Notify { get; private set; }
        public IPAddress RequestIpAddress { get; private set; }

        #endregion Properties

        #region Methods

        public bool ValidateMerchantId()
        {
            return this.ValidateMerchantIdWithChecks();
        }

#if NETFULL

        public bool ValidateSourceIp()
        {
            return this.ValidateSourceIpAddress(ipAddress: this.RequestIpAddress);
        }
#else
        public async Task<bool> ValidateSourceIp()
        {
            return await this.ValidateSourceIpAddressAsync(ipAddress: this.RequestIpAddress);
        }
#endif

        public async Task<bool> ValidateData()
        {
            return await this.ValidateDataAsync(this.Notify);
        }

        #endregion Methods

        #region Private Methods

        private bool ValidateMerchantIdWithChecks()
        {
            if (this.Settings == null)
            {
                throw new ArgumentNullException(nameof(this.Settings));
            }

            if (this.Notify == null)
            {
                throw new ArgumentNullException(nameof(this.Notify));
            }

            return this.Notify.merchant_id == this.Settings.MerchantId;
        }

#if NETFULL
        private bool ValidateSourceIpAddress(IPAddress ipAddress)
        {
            var validIps = new List<IPAddress>();

            for (int i = 0; i < PayFastStatics.ValidSites.Length; i++)
            {
                validIps.AddRange(Dns.GetHostAddresses(PayFastStatics.ValidSites[i]));
            }

            if (validIps.Contains(ipAddress))
                return true;
            else
                return false;
        }
#else
        private async Task<bool> ValidateSourceIpAddressAsync(IPAddress ipAddress)
        {
            var validIps = new List<IPAddress>();

            for (int i = 0; i < PayFastStatics.ValidSites.Length; i++)
            {
                validIps.AddRange(await Dns.GetHostAddressesAsync(PayFastStatics.ValidSites[i]));
            }

            if (validIps.Contains(ipAddress))
                return true;
            else
                return false;
        }
#endif

        private async Task<bool> ValidateDataAsync(PayFastNotify notifyViewModel)
        {
            try
            {
                var nameValueList = this.Notify.GetUnderlyingProperties();

                if (nameValueList == null)
                {
                    return false;
                }

                using (var formContent = new FormUrlEncodedContent(nameValueList))
                {
                    using (var webClient = new HttpClient())
                    {
                        using (var response = await webClient.PostAsync(this.Settings.ValidateUrl, formContent))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var result = await response.Content.ReadAsStringAsync();

                                if (result == null || !result.Equals("VALID"))
                                {
                                    return false;
                                }

                                return true;
                            }

                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Private Methods
    }
}
