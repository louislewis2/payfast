namespace PayFast.Web.Core
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using PayFast;

    public class Startup
    {
        #region Constructor

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion Constructor

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion Properties

        #region Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PayFastSettings>(this.Configuration.GetSection("PayFastSettings"));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpointRoutebuilder =>
            {
                endpointRoutebuilder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion Methods
    }
}
