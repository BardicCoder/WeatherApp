using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Services;
using ThirdPartyApiCaller.Utility;
using Microsoft.Extensions.Options;

namespace WeatherApp
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
            services.AddMvc();

            services.Configure<MySettingsModel>(Configuration.GetSection("MySettings"));

            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MySettingsModel>>().Value);

            string connStr = Configuration.GetConnectionString("WeatherAppDataBase");
            services.AddDbContext<dbContext>(options => options.UseSqlServer(connStr));
            services.AddScoped<IUserService, UserServiceViaDatabase>();
            services.AddScoped<ILocationService, LocationServiceViaWeb>();
            services.AddScoped<IWeatherForecastService, WeatherForecastServiceViaWeb>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
