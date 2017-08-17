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
            var defaultStringContent = "amount=0&signature=8e0abf3d0916134016b922eb9ed3f4fe";
            var payFastViewModel = new PayFastRequest();

            // Act
            var queryString = payFastViewModel.ToString();

            // Assert
            Assert.Equal(defaultStringContent, queryString);
        }

        [Fact]
        public void Can_Generate_Correct_Signature_With_Passphrase()
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

        [Fact]
        public void Can_Generate_Correct_Signature_Without_Passphrase_Recurring_Billing()
        {
            // Arrange
            var payFastViewModel = new PayFastRequest();

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
            Assert.Equal("8f36f2e6fc5d98e1fc609f4ba46db5c0", signature);
        }

        [Fact]
        public void Can_Generate_Correct_Signature_Without_Passphrase_Once_Off()
        {
            // Arrange
            var onceOffRequest = new PayFastRequest();

            // Merchant Details
            onceOffRequest.merchant_id = "10004241";
            onceOffRequest.merchant_key = "132ncgdwrh2by";
            onceOffRequest.return_url = "https://5ca4377c.ngrok.io/home/return";
            onceOffRequest.cancel_url = "https://5ca4377c.ngrok.io/home/cancel";
            onceOffRequest.notify_url = "https://5ca4377c.ngrok.io/home/notify";

            // Buyer Details
            onceOffRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            onceOffRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            onceOffRequest.amount = 30;
            onceOffRequest.item_name = "Once off option";
            onceOffRequest.item_description = "Some details about the once off payment";

            // Transaction Options
            onceOffRequest.email_confirmation = true;
            onceOffRequest.confirmation_address = "sbtu01@payfast.co.za";

            // Act
            var signature = onceOffRequest.signature;

            // Assert
            Assert.Equal("66d25171083fa3e36ff3ebaa3c7f0713", signature);
        }
    }
}
