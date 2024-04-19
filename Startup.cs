using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SurveyTool.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SurveyTool
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            switch (this.Configuration.GetValue<string>("AppConnectionMode", "Online"))
            {
                case "Offline":
                    services.AddDbContext<SurveyToolDbContextBase, SurveyToolSqliteDbContext>(options =>
                        options.UseSqlite(Configuration.GetConnectionString("SurveyToolOfflineDbContext")));
                    // Setup authentication with IdentityDbContext users
                    services.AddIdentity<User, IdentityRole>()
                                                .AddEntityFrameworkStores<SurveyToolDbContextBase>()
                                                .AddDefaultTokenProviders();
                    break;

                case "Online":
                default:
                    services.AddDbContext<SurveyToolDbContextBase, SurveyToolSqlServerDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("SurveyToolOnlineDbContext")));
                    break;
            }

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }
        


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                if (this.Configuration.GetValue<string>("AppConnectionMode", "Online") == "Online"){
                    app.UseHsts();
                }
                
            }

            app.UseStaticFiles();
            if (!_env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            // Use authentication and authorization
            // NOTE: This is a timing error necessity to put between UseRouting and UseEndpoints
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (_env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
