namespace PayFast.UnitTests
{
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PayFastAdHocTests : TestBase
    {
        #region Fields

        private const string token = "f6fdb89d-f60f-498a-8ffb-112bd14872dd";

        #endregion Fields

        #region Methods

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
            Assert.AreEqual(ResultStatus.Active, fetchResult.data.response.status);
        }

        [TestMethod]
        public async Task Can_Perform_Charge()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var chargeResult = await payFastIntegrationClient.Charge(token: token, amount: 5000, item_name: "test item", testing: true);

            // Assert
            Assert.AreEqual("200", chargeResult.code);
            Assert.AreEqual("success", chargeResult.status);
            Assert.AreEqual("Success", chargeResult.data.message);
            Assert.AreEqual("true", chargeResult.data.response);
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

        #endregion Methods
    }
}
