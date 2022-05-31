using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SteamGameStatistics.Cache;
using SteamGameStatistics.Data;
using SteamGameStatistics.Domain.Interfaces;
using SteamGameStatistics.Domain.Services;

namespace SteamGameStatistics
{
    public class Startup
    {
        private const string HealthEndpoint = "/health";
        private const string DatabaseConnectionName = "database";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString(DatabaseConnectionName));
            services.AddMemoryCache();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddHttpClient<ISteamService, SteamService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IEnvironmentVariablesService, EnvironmentVariablesService>();
            services.AddDbContext<GameDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(DatabaseConnectionName)));
            services.AddAutoMapper(typeof(AutoMapperProfile));
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks(HealthEndpoint);
            });

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
        }
    }
}
