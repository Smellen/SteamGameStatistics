using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Cache;
using SteamGameStatistics.Data;
using SteamGameStatistics.Interfaces;
using SteamGameStatistics.Services;

namespace SteamGameStatistics
{
    public class Startup
    {
        private const string HealthEndpoint = "/health";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<ICacheService, CacheService>();
            services.AddHttpClient<ISteamService, SteamService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IEnvironmentVariablesService, EnvironmentVariablesService>();
            services.AddDbContext<GameDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("database")));
            services.AddControllersWithViews().AddViewComponentsAsServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseHealthChecks(HealthEndpoint, "success");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
        }
    }
}
