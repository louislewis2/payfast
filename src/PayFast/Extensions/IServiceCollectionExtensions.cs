namespace Microsoft.Extensions.DependencyInjection
{
    using System;

    using PayFast;

    public static class IServiceCollectionExtensions
    {
        public static void AddPayFastIntegration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<PayFastIntegrationClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(uriString: "https://api.payfast.co.za");
            });
        }
    }
}
