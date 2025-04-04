using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalR_BackgroundService.Hubs;
using SignalR_BackgroundService.Services;

namespace SignalR_BackgroundService
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
            //services.AddControllers();
            services.AddCors(options=>{
                options.AddPolicy("AllowLocalhostClient",builder=>{
                    builder.WithOrigins("http://localhost:3000");
                    builder.AllowCredentials();
                    builder.AllowAnyHeader();
                });
            });
            services.AddSingleton<ConnectedUsers>();
            services.AddSignalR();
            services.AddHostedService<NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowLocalhostClient");    
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/hubs/notification");
                endpoints.MapGet("/",(context)=>{
                   return context.Response.WriteAsync("Get response sent from SignalR hosted in background services");
                });
            });
        }
    }
}
