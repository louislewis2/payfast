namespace PayFast.UnitTests
{
    using System;

    using Xunit;

    public class RequestTests
    {
        [Fact]
        public void Can_Handle_Defaults()
        {
            // Arrange
            var defaultStringContent = "amount=0&signature=dee1b01be385c8ea762109a076fb12b6";
            var payFastViewModel = new PayFastRequest();

            // Act
            var queryString = payFastViewModel.ToString();

            // Assert
            Assert.Equal(defaultStringContent, queryString);
        }

        [Fact]
        public void Can_Generate_Correct_Signature()
        {
            // Arrange
            var passPhrase = "salt";
            var payFastViewModel = new PayFastRequest(passPhrase);

            // Merchant Details
            payFastViewModel.merchant_id = "10004241";
            payFastViewModel.merchant_key = "132ncgdwrh2by";
            payFastViewModel.return_url = "http://f32149e3.ngrok.io/processing/successful";
            payFastViewModel.cancel_url = "http://f32149e3.ngrok.io/processing/cancel";
            payFastViewModel.notify_url = "http://f32149e3.ngrok.io/processing/notify";

            // Buyer Details
            payFastViewModel.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            payFastViewModel.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            payFastViewModel.amount = 20;
            payFastViewModel.item_name = "Option 2";
            payFastViewModel.item_description = "Some details about option 2";

            // Transaction Options
            payFastViewModel.email_confirmation = true;
            payFastViewModel.confirmation_address = "sbtu01@payfast.co.za";

            // Recurring Billing Details
            payFastViewModel.subscription_type = SubscriptionType.Subscription;
            payFastViewModel.billing_date = new DateTime(2017, 02, 14);
            payFastViewModel.recurring_amount = 20;
            payFastViewModel.frequency = BillingFrequency.Monthly;
            payFastViewModel.cycles = 0;

            // Act
            var signature = payFastViewModel.signature;

            // Assert
            Assert.Equal("e11880438cdc68addba56f65d80d27a6", signature);
        }
    }
}
