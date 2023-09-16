namespace PayFast.UnitTests
{
    using System.Threading.Tasks;

    using Xunit;

    public class PayFastAdHocTests : TestBase
    {
        #region Fields

        private const string token = "f6fdb89d-f60f-498a-8ffb-112bd14872dd";

        #endregion Fields

        #region Methods

        [Fact(Skip = "Skip when running under ci")]
        public async Task Can_Perform_Fetch()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var fetchResult = await payFastIntegrationClient.Fetch(token: token, testing: true);

            // Assert
            Assert.Equal("200", fetchResult.code);
            Assert.Equal("success", fetchResult.status);
            Assert.Equal(ResultStatus.Active, fetchResult.data.response.status);
        }

        [Fact(Skip = "Skip when running under ci")]
        public async Task Can_Perform_Charge()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var chargeResult = await payFastIntegrationClient.Charge(token: token, amount: 5000, item_name: "test item", testing: true);

            // Assert
            Assert.Equal("200", chargeResult.code);
            Assert.Equal("success", chargeResult.status);
            Assert.Equal("Success", chargeResult.data.message);
            Assert.Equal("true", chargeResult.data.response);
        }

        [Fact(Skip = "Skip when running under ci")]
        public async Task Can_Perform_Cancel()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var cancelResult = await payFastIntegrationClient.Cancel(token: token, testing: true);

            // Assert
            Assert.Equal("200", cancelResult.code);
            Assert.Equal("success", cancelResult.status);
            Assert.Equal("true", cancelResult.data.response);
            Assert.Equal("Success", cancelResult.data.message);
        }

        #endregion Methods
    }
}
