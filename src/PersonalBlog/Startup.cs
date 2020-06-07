using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalBlog.Filters;
using PersonalBlog.Interfaces;
using PersonalBlog.Models;
using PersonalBlog.Services;

namespace PersonalBlog
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
            services.AddControllersWithViews();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizer, IpBasedAuthorizer>();
            services.AddScoped<ProtectorAttribute>();
            services.AddLogging(c => c.AddConsole());

            ConfigureDataServices(services);
        }

        private static void ConfigureDataServices(IServiceCollection services)
        {
            services.AddSingleton<InMemoryDataService>();
            services.AddSingleton<InMemoryDataServiceV2>();

            services.AddSingleton<Func<VersionEnum, IDataService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case VersionEnum.V1: return serviceProvider.GetService<InMemoryDataService>();
                    case VersionEnum.V2: return serviceProvider.GetService<InMemoryDataServiceV2>();
                    default: return null;
                }
            });
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
