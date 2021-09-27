using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantBlazor.Data;
using MultiTenantBlazor.Models.Configuration;
using MultiTenantBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Helpers.Middleware
{
    public static class MigrationApplicationBuilder
    {

        public static IApplicationBuilder DbMigrationRunner(this IApplicationBuilder app)
        {
            // This will update all tenant's databases with the current migrations. This is based on the ApplyDbMigrationsOnStartup configuration in appsettings.
            // If this is true all tenant DBs will be updated (including localhost). If false then you can use standard EF Core tooling to create, update and remove migrations.

            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();
            var applyMigrationsOnStartup = config.GetValue<bool>("Finbuckle:MultiTenant:Stores:ConfigurationStore:ApplyDbMigrationsOnStartup");

            if (applyMigrationsOnStartup)
            {
                var _tenantService = app.ApplicationServices.GetRequiredService<ITenantListService>();
                var allTenants = _tenantService.GetAllTenants();

                foreach (var tenant in allTenants)
                {
                    var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
                    var sqlOptions = dbOptions.UseSqlServer(tenant.ConnectionString).Options;
                    using (var db = new ApplicationDbContext(tenant, sqlOptions))
                    {
                        db.Database.Migrate();
                    }
                }
            }

            return app;
        }
    }
}
