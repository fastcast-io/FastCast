using System;
using FastCast.Areas.Identity.Data;
using FastCast.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FastCast.Areas.Identity.IdentityHostingStartup))]
namespace FastCast.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FastCastUserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FastCastUserContextConnection")));

                services.AddDefaultIdentity<FastCastUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<FastCastUserContext>();
            });
        }
    }
}