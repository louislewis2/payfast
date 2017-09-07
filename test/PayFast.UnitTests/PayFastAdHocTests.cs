namespace PayFast.UnitTests
{
    using System.Threading.Tasks;

    using Xunit;

    public class PayFastAdHocTests : TestBase
    {
        #region Fields

        private const string token = "e8cbf06b-b70f-4f42-d595-3aeae694117d";

        #endregion Fields

        [Fact]
        public async Task Can_Perform_Ping()
        {
            // Arrange
            var payfastAdhoc = new PayFastAdHoc(payfastSettings: this.GetTestSettings());

            // Act
            var pingResult = await payfastAdhoc.Ping(token: token, testing: true);

            // Assert
            Assert.Equal(true, pingResult);
        }

        [Fact]
        public async Task Can_Perform_Fetch()
        {
            // Arrange
            var payfastAdhoc = new PayFastAdHoc(payfastSettings: this.GetTestSettings());

            // Act
            var fetchResult = await payfastAdhoc.Fetch(token: token, testing: true);

            // Assert
            Assert.Equal("200", fetchResult.code);
            Assert.Equal("success", fetchResult.status);
            Assert.Equal(ResultStatus.Active, fetchResult.data.response.status);
        }

        [Fact]
        public async Task Can_Perform_Charge()
        {
            // Arrange
            var payfastAdhoc = new PayFastAdHoc(payfastSettings: this.GetTestSettings());

            // Act
            var chargeResult = await payfastAdhoc.Charge(token: token, amount: 5000, item_name: "test item", testing: true);

            // Assert
            Assert.Equal("200", chargeResult.code);
            Assert.Equal("success", chargeResult.status);
            Assert.Equal("Success", chargeResult.data.message);
            Assert.Equal("true", chargeResult.data.response);
        }

        [Fact]
        public async Task Can_Perform_Cancel()
        {
            // Arrange
            var payfastAdhoc = new PayFastAdHoc(payfastSettings: this.GetTestSettings());

            // Act
            var cancelResult = await payfastAdhoc.Cancel(token: token, testing: true);

            // Assert
            Assert.Equal("200", cancelResult.code);
            Assert.Equal("success", cancelResult.status);
            Assert.Equal("true", cancelResult.data.response);
            Assert.Equal("Success", cancelResult.data.message);
        }
    }
}
