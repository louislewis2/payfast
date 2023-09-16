namespace PayFast.UnitTests
{
    using System.Threading.Tasks;

    using Xunit;

    public class PingTests : TestBase
    {
        #region Methods

        [Fact]
        public async Task Can_Perform_Ping()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var pingResult = await payFastIntegrationClient.Ping(testing: true);

            // Assert
            Assert.Equal(expected: "PayFast API", actual: pingResult);
        }

        #endregion Methods
    }
}
