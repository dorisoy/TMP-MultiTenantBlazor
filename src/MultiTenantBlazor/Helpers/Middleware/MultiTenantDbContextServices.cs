using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantBlazor.Data;
using MultiTenantBlazor.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Helpers.Middleware
{
    public static class MultiTenantDbContextServices
    {
        public static IServiceCollection AddMultiTenantDbContext(this IServiceCollection services, IConfiguration config)
        {
            // This is used so we can use EF Core migrations. You can use the Package Manager Console to create new migrations without any compile errors if this is true.
            // You can re-enable after migrations by this being false. If false it will connect to individual tenant DBs.
            // If this is also false the application will also migrate all migrations per tenant DB. See app.DbMigrationRunner() in startup.

            var forceLocalOnlyDbConnection = config.GetValue<bool>("Finbuckle:MultiTenant:Stores:ConfigurationStore:ForceLocalOnlyDbConnection");

            services.AddDbContext<ApplicationDbContext>((services, options) => {
                var env = services.GetRequiredService<IWebHostEnvironment>();
                if (!forceLocalOnlyDbConnection)
                {
                    var currentTenant = services.GetService<IHttpContextAccessor>().HttpContext.GetMultiTenantContext<TenantInfo>()?.TenantInfo;
                    options.UseSqlServer(currentTenant.ConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(int.MaxValue));
                }
                else
                {
                    var localConnectionString = config.GetValue<string>("Finbuckle:MultiTenant:Stores:ConfigurationStore:Tenants:0:ConnectionString");
                    options.UseSqlServer(localConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(int.MaxValue));
                }
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            return services;
        }
    }
}
