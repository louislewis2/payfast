namespace PayFast.UnitTests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class TestBase
    {
        #region Fields

        private readonly IServiceProvider serviceProvider;

        #endregion Fields

        #region Constructor

        public TestBase()
        {
            var serviceCollection = new ServiceCollection();

            this.ConfigureServices(serviceCollection: serviceCollection);

            this.serviceProvider = serviceCollection.BuildServiceProvider();
        }

        #endregion Constructor

        #region Methods

        public T GetRequiredService<T>()
        {
            return this.serviceProvider.GetService<T>();
        }

        protected PayFastSettings GetTestSettings()
        {
            return new PayFastSettings
            {
                MerchantId = "10004241",
                MerchantKey = "132ncgdwrh2by",
                PassPhrase = "salt"
            };
        }

        #endregion Methods

        #region Private Methods

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.Configure<PayFastSettings>(payfastSettings =>
            {
                payfastSettings.MerchantId = "10004241";
                payfastSettings.MerchantKey = "132ncgdwrh2by";
                payfastSettings.PassPhrase = "salt";
            });

            serviceCollection.AddPayFastIntegration();
        }

        #endregion Private Methods
    }
}
