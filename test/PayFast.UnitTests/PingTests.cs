namespace PayFast.UnitTests
{
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PingTests : TestBase
    {
        #region Methods

        [TestMethod]
        public async Task Can_Perform_Ping()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var pingResult = await payFastIntegrationClient.Ping(testing: true);

            // Assert
            Assert.AreEqual(expected: "PayFast API", actual: pingResult);
        }

        #endregion Methods
    }
}
