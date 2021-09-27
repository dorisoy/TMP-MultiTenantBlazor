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
            // This is used so we can use EF Core migrations. If the HttpContext is null (like using EF Core tooling) then we'll use the first tenant connection string (localhost).
            // Migrations are applied on startup to all tenants dependent on configuration. See Helpers > Middleware > MigrationApplicationBuilder.cs

            services.AddDbContext<ApplicationDbContext>((services, options) => {
                var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
                if (httpContextAccessor.HttpContext != null)
                {
                    var currentTenant = httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>()?.TenantInfo;
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
