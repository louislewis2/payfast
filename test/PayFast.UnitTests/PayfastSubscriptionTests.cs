namespace PayFast.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PayfastSubscriptionTests : TestBase
    {
        #region Fields

        private const string token = "c909b006-27d2-444b-a37d-940651f39329";

        #endregion Fields

        [TestMethod]
        public async Task Can_Perform_Fetch()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var fetchResult = await payFastIntegrationClient.Fetch(token: token, testing: true);

            // Assert
            Assert.AreEqual("200", fetchResult.code);
            Assert.AreEqual("success", fetchResult.status);
            Assert.AreEqual(3000, fetchResult.data.response.amount);
            Assert.AreEqual(3, fetchResult.data.response.cycles);
            Assert.AreEqual(1, fetchResult.data.response.cycles_complete);
            Assert.AreEqual(BillingFrequency.Annual, fetchResult.data.response.frequency);
            Assert.AreEqual(DateTime.Parse("2024-07-28T00:00:00+02:00"), fetchResult.data.response.run_date);
            Assert.AreEqual(ResultStatus.Active, fetchResult.data.response.status);
            Assert.AreEqual(token, fetchResult.data.response.token);
        }

        [TestMethod]
        public async Task Can_Perform_Update()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var updateResult = await payFastIntegrationClient.Update(
                token: token, 
                cycles: 3, 
                frequency: BillingFrequency.Biannual, 
                run_date: new DateTime(2024, 10, 28),
                amount: 3000, 
                testing: true);

            // Assert
            Assert.AreEqual("200", updateResult.code);
            Assert.AreEqual("success", updateResult.status);
            Assert.AreEqual(3000, updateResult.data.response.amount);
            Assert.AreEqual(3, updateResult.data.response.cycles);
            Assert.AreEqual(0, updateResult.data.response.cycles_complete);
            Assert.AreEqual(BillingFrequency.Biannual, updateResult.data.response.frequency);
            Assert.AreEqual(DateTime.Parse("2024-10-28T00:00:00+02:00"), updateResult.data.response.run_date);
            Assert.AreEqual(ResultStatus.Active, updateResult.data.response.status);
            Assert.AreEqual(token, updateResult.data.response.token);
        }

        [TestMethod]
        public async Task Can_Perform_Pause_For_OneCycle()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var updateResult = await payFastIntegrationClient.Pause(
                token: token,
                testing: true);

            // Assert
            Assert.AreEqual("200", updateResult.code);
            Assert.AreEqual("success", updateResult.status);
            Assert.AreEqual("true", updateResult.data.response);
        }

        [TestMethod]
        public async Task Can_Perform_UnPause()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var updateResult = await payFastIntegrationClient.UnPause(
                token: token,
                testing: true);

            // Assert
            Assert.AreEqual("200", updateResult.code);
            Assert.AreEqual("success", updateResult.status);
            Assert.AreEqual("true", updateResult.data.response);
        }

        [TestMethod]
        public async Task Can_Perform_Pause_For_Cycles()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var updateResult = await payFastIntegrationClient.Pause(
                token: token,
                cycles: 3,
                testing: true);

            // Assert
            Assert.AreEqual("200", updateResult.code);
            Assert.AreEqual("success", updateResult.status);
            Assert.AreEqual("true", updateResult.data.response);
        }

        [TestMethod]
        public async Task Can_Perform_Cancel()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var cancelResult = await payFastIntegrationClient.Cancel(token: token, testing: true);

            // Assert
            Assert.AreEqual("200", cancelResult.code);
            Assert.AreEqual("success", cancelResult.status);
            Assert.AreEqual("true", cancelResult.data.response);
            Assert.AreEqual("Success", cancelResult.data.message);
        }
    }
}
