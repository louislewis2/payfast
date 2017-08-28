namespace PayFast.ApiTypes
{
    using System;

    public class SubscriptionDetailResponse
    {
        #region Properties

        public string token { get; set; }
        public int amount { get; set; }
        public int cycles { get; set; }
        public int cycles_complete { get; set; }
        public BillingFrequency frequency { get; set; }
        public ResultStatus status { get; set; }
        public DateTime run_date { get; set; }

        #endregion Properties
    }
}
