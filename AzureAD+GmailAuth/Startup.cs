using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using AzureAD_GmailAuth.Models;

namespace AzureAD_GmailAuth
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
            //RG if i don't use the below code the Dependency Injection setup throws exception during runtime
            services.AddIdentity<IdentityUser,IdentityRole>().AddUserStore<UserStore>().AddRoleStore<RoleStore>()                                        
                    .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<IdentityUser>,UserStore>();
            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions=>{
                microsoftOptions.ClientId = Configuration["MicrosoftAuth:ClientId"];
                microsoftOptions.ClientSecret = Configuration["MicrosoftAuth:ClientSecret"];   
                microsoftOptions.SaveTokens=true;                
            })
            .AddGoogle(googleOptions=>{
                googleOptions.ClientId = Configuration["GoogleAuth:ClientId"];
                googleOptions.ClientSecret = Configuration["GoogleAuth:ClientSecret"];   
                googleOptions.SaveTokens=true;                 
            
            });            
            services.AddRazorPages().AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });            
            services.AddControllers().AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //RG  don't do the below during prod 
                app.UseDeveloperExceptionPage();
                // app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();                
                //if provider calls AccountController Login action , redirect to Index page
                endpoints.MapGet("/Account/Login",(context)=>Task.Run(()=>context.Response.Redirect("/")));                
            });        
        }
    }
}
