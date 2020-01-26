using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreVuejs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //the below is for deployed version
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "ClientApp\\dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //to test deployment version comment the if alone
            if (!env.IsDevelopment())
            {
                //used for deployed version
                app.UseSpaStaticFiles();
            }                      

            app.UseSpa(spa =>
            {
                // To learn  about options for serving an  SPA from ASP.NET Core, (in link example is angular, we use Vuejs)
                // see https://go.microsoft.com/fwlink/?linkid=864501
                //to test deployed version comment below lines of code
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080/");
                }

            });
        }
    }
}
