using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SisGerenciador.Areas.Identity.Data;
using SisGerenciador.Data;

[assembly: HostingStartup(typeof(SisGerenciador.Areas.Identity.IdentityHostingStartup))]
namespace SisGerenciador.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SisGerenciadorContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MeuContexto")));

                services.AddDefaultIdentity<SisGerenciadorUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()    
                    .AddEntityFrameworkStores<SisGerenciadorContext>();
            });
        }
    }
}