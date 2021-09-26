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
            // This will update all tenant's databases with the current migrations. You can stop this if forceLocalOnlyDbConnection is false.
            // This setting is mainly for local testing but also to enable individual migrations using standard EF Core tooling.

            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();
            var forceLocalOnlyDbConnection = config.GetValue<bool>("Finbuckle:MultiTenant:Stores:ConfigurationStore:ForceLocalOnlyDbConnection");

            if (!forceLocalOnlyDbConnection)
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
