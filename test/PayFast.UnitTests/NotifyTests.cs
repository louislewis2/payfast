namespace PayFast.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NotifyTests
    {
        [TestMethod, TestCategory(testCategory: "RunAlways")]
        public void Can_Verify_Signature_With_Passphrase()
        {
            // Arrange
            var passPhrase = "salt";
            var notifyViewModel = new PayFastNotify();
            notifyViewModel.SetPassPhrase(passPhrase);
            notifyViewModel.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            notifyViewModel.pf_payment_id = "1847925";
            notifyViewModel.payment_status = "COMPLETE";
            notifyViewModel.item_name = "Recurring Option";
            notifyViewModel.item_description = "Some details about the recurring option";
            notifyViewModel.amount_gross = "20.00";
            notifyViewModel.amount_fee = "-2.30";
            notifyViewModel.amount_net = "17.70";
            notifyViewModel.custom_str1 = string.Empty;
            notifyViewModel.custom_str2 = string.Empty;
            notifyViewModel.custom_str3 = string.Empty;
            notifyViewModel.custom_str4 = string.Empty;
            notifyViewModel.custom_str5 = string.Empty;
            notifyViewModel.custom_int1 = string.Empty;
            notifyViewModel.custom_int2 = string.Empty;
            notifyViewModel.custom_int3 = string.Empty;
            notifyViewModel.custom_int4 = string.Empty;
            notifyViewModel.custom_int5 = string.Empty;
            notifyViewModel.name_first  = string.Empty;
            notifyViewModel.name_last   = string.Empty;
            notifyViewModel.email_address = "sbtu01@payfast.co.za";
            notifyViewModel.merchant_id = "10004241";
            notifyViewModel.token = "5a538bc0-ce28-47d4-98bd-9ee7bc11ad56";
            notifyViewModel.billing_date = "2023-09-16";
            notifyViewModel.signature = "bf1986d6bed6b382e0f88f32a92fee03";

            // Act
            var calculatedSignature = notifyViewModel.GetCalculatedSignature();

            // Assert
            Assert.AreEqual(notifyViewModel.signature, calculatedSignature);
        }

        [TestMethod, TestCategory(testCategory: "RunAlways")]
        public void Can_Verify_Signature_Without_Passphrase()
        {
            // Arrange
            var notifyViewModel = new PayFastNotify();
            notifyViewModel.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            notifyViewModel.pf_payment_id = "1847945";
            notifyViewModel.payment_status = "COMPLETE";
            notifyViewModel.item_name = "Once off option";
            notifyViewModel.item_description = "Some details about the once off payment";
            notifyViewModel.amount_gross = "30.00";
            notifyViewModel.amount_fee = "-2.30";
            notifyViewModel.amount_net = "27.70";
            notifyViewModel.custom_str1 = string.Empty;
            notifyViewModel.custom_str2 = string.Empty;
            notifyViewModel.custom_str3 = string.Empty;
            notifyViewModel.custom_str4 = string.Empty;
            notifyViewModel.custom_str5 = string.Empty;
            notifyViewModel.custom_int1 = string.Empty;
            notifyViewModel.custom_int2 = string.Empty;
            notifyViewModel.custom_int3 = string.Empty;
            notifyViewModel.custom_int4 = string.Empty;
            notifyViewModel.custom_int5 = string.Empty;
            notifyViewModel.name_first = string.Empty;
            notifyViewModel.name_last = string.Empty;
            notifyViewModel.email_address = "sbtu01@payfast.co.za";
            notifyViewModel.merchant_id = "10004241";
            notifyViewModel.signature = "94ea076d95918eb0661f37ecbf206552";

            // Act
            var calculatedSignature = notifyViewModel.GetCalculatedSignature();

            // Assert
            Assert.AreEqual(notifyViewModel.signature, calculatedSignature);
        }

        [TestMethod, TestCategory(testCategory: "RunAlways")]
        public void Can_Verify_Charge_Signature_With_Passphrase()
        {
            // Arrange
            var notifyViewModel = new PayFastNotify();
            notifyViewModel.SetPassPhrase("salt");
            notifyViewModel.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            notifyViewModel.token = "9aff14eb-65ea-a8ff-ca41-ef457c9c351a";
            notifyViewModel.pf_payment_id = "392638";
            notifyViewModel.payment_status = "COMPLETE";
            notifyViewModel.item_name = "Recurring Option";
            notifyViewModel.item_description = "Some details about the recurring option";
            notifyViewModel.amount_gross = "20.00";
            notifyViewModel.amount_fee = "-3.17";
            notifyViewModel.amount_net = "16.83";
            notifyViewModel.custom_str1 = string.Empty;
            notifyViewModel.custom_str2 = string.Empty;
            notifyViewModel.custom_str3 = string.Empty;
            notifyViewModel.custom_str4 = string.Empty;
            notifyViewModel.custom_str5 = string.Empty;
            notifyViewModel.custom_int1 = string.Empty;
            notifyViewModel.custom_int2 = string.Empty;
            notifyViewModel.custom_int3 = string.Empty;
            notifyViewModel.custom_int4 = string.Empty;
            notifyViewModel.custom_int5 = string.Empty;
            notifyViewModel.name_first = "Test";
            notifyViewModel.name_last = "User 01";
            notifyViewModel.email_address = "sbtu01@payfast.co.za";
            notifyViewModel.merchant_id = "10004241";
            
            notifyViewModel.signature = "c629a4b9c9df461a29db663ec0a77ae7";

            // Act
            var calculatedSignature = notifyViewModel.GetCalculatedSignature();

            // Assert
            Assert.AreEqual(notifyViewModel.signature, calculatedSignature);
        }
    }
}
