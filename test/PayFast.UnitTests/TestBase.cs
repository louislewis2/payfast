namespace PayFast.UnitTests
{
    public class TestBase
    {
        #region Methods

        protected PayFastSettings GetTestSettings()
        {
            return new PayFastSettings
            {
                MerchantId = "10004241",
                MerchantKey = "132ncgdwrh2by",
                PassPhrase = "salt"
            };
        }

        #endregion Methods
    }
}
