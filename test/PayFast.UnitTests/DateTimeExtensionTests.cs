namespace PayFast.UnitTests
{
    using System;

    using Xunit;

    public class DateTimeExtensionTests
    {
        [Fact]
        public void Can_Return_Correct_Date_String()
        {
            // Arrange
            var date = new DateTime(2017, 02, 13);

            // Act
            var dateString = date.ToPayFastDate();

            // Assert
            Assert.Equal("2017-02-13", dateString);
        }
    }
}
