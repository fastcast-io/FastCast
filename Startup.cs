using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FastCast.Models;
using SmartBreadcrumbs.Extensions;

namespace FastCast
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
            services.AddRazorPages();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var googleAuthNSection = 
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            var connectionString = Configuration.GetConnectionString("FastCastContext");

            services.AddDbContext<FastCastContext>(options =>
                    options.UseSqlServer(connectionString));
            //services.AddDbContext<FastCastContext>(options =>
            //          options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

            services.AddSingleton<IFastCastService, FastCastService>();
            //services.AddBreadcrumbs(GetType().Assembly, options =>
            //{
            //    options.TagName = "nav";
            //    options.TagClasses = "";
            //    options.OlClasses = "breadcrumb";
            //    options.LiClasses = "breadcrumb-item";
            //    options.ActiveLiClasses = "breadcrumb-item active";
            //    options.SeparatorElement = "<li class=\"separator\">/</li>";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
