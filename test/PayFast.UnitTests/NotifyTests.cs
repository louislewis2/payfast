namespace PayFast.UnitTests
{
    using Xunit;

    public class NotifyTests
    {
        [Fact]
        public void Can_Verify_Signature_With_Passphrase()
        {
            // Arrange
            var passPhrase = "salt";
            var notifyViewModel = new PayFastNotify();
            notifyViewModel.SetPassPhrase(passPhrase);
            notifyViewModel.amount_fee = "0.00";
            notifyViewModel.amount_gross = "20.00";
            notifyViewModel.amount_net = "20.00";
            notifyViewModel.billing_date = "2017-02-13";
            notifyViewModel.email_address = "sbtu01@payfast.co.za";
            notifyViewModel.item_description = "Some details about option 2";
            notifyViewModel.item_name = "Option 2";
            notifyViewModel.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            notifyViewModel.merchant_id = "10004241";
            notifyViewModel.name_first = "Test";
            notifyViewModel.name_last = "User 01";
            notifyViewModel.payment_status = "COMPLETE";
            notifyViewModel.pf_payment_id = "327767";
            notifyViewModel.signature = "c5cce9e08316373ca2ba6b427e39772e";
            notifyViewModel.token = "01c3f68f-5802-4760-c0a5-85d658ccff99";

            // Act
            var calculatedSignature = notifyViewModel.GetCalculatedSignature();

            // Assert
            Assert.Equal(notifyViewModel.signature, calculatedSignature);
        }

        [Fact]
        public void Can_Verify_Signature_Without_Passphrase()
        {
            // Arrange
            var notifyViewModel = new PayFastNotify();
            notifyViewModel.amount_fee = "-2.28";
            notifyViewModel.amount_gross = "30.00";
            notifyViewModel.amount_net = "27.72";
            notifyViewModel.email_address = "sbtu01@payfast.co.za";
            notifyViewModel.item_description = "Some details about the once off payment";
            notifyViewModel.item_name = "Once off option";
            notifyViewModel.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            notifyViewModel.merchant_id = "10004241";
            notifyViewModel.name_first = "Test";
            notifyViewModel.name_last = "User 01";
            notifyViewModel.payment_status = "COMPLETE";
            notifyViewModel.pf_payment_id = "392624";
            notifyViewModel.signature = "a8a4d344281414843db03880265e3d94";

            // Act
            var calculatedSignature = notifyViewModel.GetCalculatedSignature();

            // Assert
            Assert.Equal(notifyViewModel.signature, calculatedSignature);
        }

        [Fact]
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
            Assert.Equal(notifyViewModel.signature, calculatedSignature);
        }
    }
}
