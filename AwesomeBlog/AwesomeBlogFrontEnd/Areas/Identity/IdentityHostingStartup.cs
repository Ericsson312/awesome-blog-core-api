using System;
using AwesomeBlogFrontEnd.Areas.Identity.Data;
using AwesomeBlogFrontEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AwesomeBlogFrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace AwesomeBlogFrontEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityDbContextConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
            });
        }
    }
}