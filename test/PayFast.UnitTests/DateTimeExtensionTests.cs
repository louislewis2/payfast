namespace PayFast.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void Can_Return_Correct_Date_String()
        {
            // Arrange
            var date = new DateTime(2017, 02, 13);

            // Act
            var dateString = date.ToPayFastDate();

            // Assert
            Assert.AreEqual("2017-02-13", dateString);
        }
    }
}
