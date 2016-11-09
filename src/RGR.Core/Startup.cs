using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RGR.Core.Models;


namespace RGR.Core
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            string AppPath = env.ContentRootPath;
            string wwwRootPath = env.WebRootPath;
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // получение строки подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");

            // добавление контекста rgrContext в качестве сервиса в приложение
            services.AddDbContext<rgrContext>(options =>
                options.UseSqlServer(connection));

            //Кэш памяти и сессии
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieName = ".RGR.Core.Session";
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages("text/plain", "Ошибка : {0}");

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute("search", "search", new { controller = "Search", action = "Search" });
                routes.MapRoute("info", "info" , new { controller = "Search", action = "Info" });
                routes.MapRoute("saveRequest", "saveRequest", new { controller = "Search", action = "SaveRequest" });
                routes.MapRoute("personal", "personal", new { controller = "Account", action = "Personal" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
