namespace PayFast
{
    public class PayFastSettings
    {
        #region Properties

        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public string PassPhrase { get; set; }
        public string ProcessUrl { get; set; }
        public string ValidateUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string NotifyUrl { get; set; }

        #endregion Properties
    }
}
