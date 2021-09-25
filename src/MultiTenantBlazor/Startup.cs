using MultiTenantBlazor.Areas.Identity;
using MultiTenantBlazor.Data;
using MultiTenantBlazor.Helpers.Identity;
using MultiTenantBlazor.Helpers.Middleware;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantBlazor
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {

               options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(int.MaxValue));
                }, ServiceLifetime.Transient);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<DataProtectionTokenProviderOptions>(
                    x => x.TokenLifespan = TimeSpan.FromDays(7));

            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                o.DetailedErrors = true;
            });

            services.RegisterApplicationSpecificServices(Configuration);

            services.AddMultiTenant<TenantInfo>()
                    .WithConfigurationStore()
                    .WithHostStrategy("__tenant__.*");

            services.AddHttpContextAccessor();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMultiTenant();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapIdentityRedirects();
            });
        }
    }
}
