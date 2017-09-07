namespace PayFast.UnitTests
{
    using System;
    using System.Threading.Tasks;

    using Xunit;

    public class PayfastSubscriptionTests : TestBase
    {
        #region Fields

        private const string token = "4e8ca2a7-4c1f-324f-09d2-b2fee375c082";

        #endregion Fields

        [Fact]
        public async Task Can_Perform_Ping()
        {
            // Arrange
            var payfastSubscription = new PayFastSubscription(payfastSettings: this.GetTestSettings());

            // Act
            var pingResult = await payfastSubscription.Ping(token: token, testing: true);

            // Assert
            Assert.Equal(true, pingResult);
        }

        [Fact]
        public async Task Can_Perform_Fetch()
        {
            // Arrange
            var payfastSubscription = new PayFastSubscription(payfastSettings: this.GetTestSettings());

            // Act
            var fetchResult = await payfastSubscription.Fetch(token: token, testing: true);

            // Assert
            Assert.Equal("200", fetchResult.code);
            Assert.Equal("success", fetchResult.status);
            Assert.Equal(2000, fetchResult.data.response.amount);
            Assert.Equal(0, fetchResult.data.response.cycles);
            Assert.Equal(1, fetchResult.data.response.cycles_complete);
            Assert.Equal(BillingFrequency.Monthly, fetchResult.data.response.frequency);
            Assert.Equal(DateTime.Parse("2017-10-07T00:00:00+02:00"), fetchResult.data.response.run_date);
            Assert.Equal(ResultStatus.Paused, fetchResult.data.response.status);
            Assert.Equal(token, fetchResult.data.response.token);
        }

        [Fact]
        public async Task Can_Perform_Update()
        {
            // Arrange
            var payfastSubscription = new PayFastSubscription(payfastSettings: this.GetTestSettings());

            // Act
            var updateResult = await payfastSubscription.Update(
                token: token, 
                cycles: 3, 
                frequency: BillingFrequency.Biannual, 
                run_date: new DateTime(2017, 10, 15),
                amount: 3000, 
                testing: true);

            // Assert
            Assert.Equal("200", updateResult.code);
            Assert.Equal("success", updateResult.status);
            Assert.Equal(3000, updateResult.data.response.amount);
            Assert.Equal(3, updateResult.data.response.cycles);
            Assert.Equal(1, updateResult.data.response.cycles_complete);
            Assert.Equal(BillingFrequency.Biannual, updateResult.data.response.frequency);
            Assert.Equal(DateTime.Parse("2017-10-15T00:00:00+02:00"), updateResult.data.response.run_date);
            Assert.Equal(ResultStatus.Paused, updateResult.data.response.status);
            Assert.Equal(token, updateResult.data.response.token);
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
