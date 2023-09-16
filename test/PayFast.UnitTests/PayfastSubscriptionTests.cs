namespace PayFast.UnitTests
{
    using System;
    using System.Threading.Tasks;

    using Xunit;

    public class PayfastSubscriptionTests : TestBase
    {
        #region Fields

        private const string token = "c909b006-27d2-444b-a37d-940651f39329";

        #endregion Fields

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
            Assert.Equal(758655, fetchResult.data.response.amount);
            Assert.Equal(0, fetchResult.data.response.cycles);
            Assert.Equal(1, fetchResult.data.response.cycles_complete);
            Assert.Equal(BillingFrequency.Annual, fetchResult.data.response.frequency);
            Assert.Equal(DateTime.Parse("2024-07-28T00:00:00+02:00"), fetchResult.data.response.run_date);
            Assert.Equal(ResultStatus.Active, fetchResult.data.response.status);
            Assert.Equal(token, fetchResult.data.response.token);
        }

        [Fact(Skip = "Skip when running under ci")]
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
            Assert.Equal("200", updateResult.code);
            Assert.Equal("success", updateResult.status);
            Assert.Equal(3000, updateResult.data.response.amount);
            Assert.Equal(3, updateResult.data.response.cycles);
            Assert.Equal(0, updateResult.data.response.cycles_complete);
            Assert.Equal(BillingFrequency.Biannual, updateResult.data.response.frequency);
            Assert.Equal(DateTime.Parse("2024-10-28T00:00:00+02:00"), updateResult.data.response.run_date);
            Assert.Equal(ResultStatus.Active, updateResult.data.response.status);
            Assert.Equal(token, updateResult.data.response.token);
        }

        [Fact(Skip = "Skip when running under ci")]
        public async Task Can_Perform_Pause_For_OneCycle()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var updateResult = await payFastIntegrationClient.Pause(
                token: token,
                testing: true);

            // Assert
            Assert.Equal("200", updateResult.code);
            Assert.Equal("success", updateResult.status);
            Assert.Equal("true", updateResult.data.response);
        }

        [Fact(Skip = "Skip when running under ci")]
        public async Task Can_Perform_UnPause()
        {
            // Arrange
            var payFastIntegrationClient = this.GetRequiredService<PayFastIntegrationClient>();

            // Act
            var updateResult = await payFastIntegrationClient.UnPause(
                token: token,
                testing: true);

            // Assert
            Assert.Equal("200", updateResult.code);
            Assert.Equal("success", updateResult.status);
            Assert.Equal("true", updateResult.data.response);
        }

        [Fact(Skip = "Skip when running under ci")]
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
            Assert.Equal("200", updateResult.code);
            Assert.Equal("success", updateResult.status);
            Assert.Equal("true", updateResult.data.response);
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
    }
}
