using System.Reflection;

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

        /// <summary>
        /// Creates a copy of the current PayFastSettings instance.
        /// </summary>
        public virtual PayFastSettings Clone()
        {
            PayFastSettings pfsCopy = new PayFastSettings();
            foreach (var prop in this.GetType().GetProperties())
            {
                PropertyInfo propInfo = pfsCopy.GetType().GetProperty(prop.Name);
                var propValue = prop.GetValue(this, null);
                propInfo.SetValue(pfsCopy, prop.GetValue(this, null), null);
            }

            return pfsCopy;
        }
    }
}