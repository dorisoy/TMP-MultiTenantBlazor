# MultiTenantBlazor

Contains an example Blazor Server application that is multi-tenanted with data isolation per tenant.

## Description

The solution is split a single project within the src folder:
- MultiTenantBlazor

It uses the Finbuckle.MultiTenant.AspNetCore NuGet package for most of the tenant configuration but also supports EF Core tooling* and migrations*.
Tenants are defined in appsettings under "Finbuckle:MultiTenant:Stores:ConfigurationStore". There is an array of "Tenants" which contains the id, identifier, name and connection string of its DB. The first tenant within the array must be kept as localhost as it is used in development and when you need to us EF Core tooling to create migrations. 

The ApplyDbMigrationsOnStartup value will apply all migrations across all tenant DBs (including localhost). Regardless of this option you can still use EF Core tooling commands like "Add-Migration". Doing this will default the migration to the localhost tenant (1st tenant in above mentioned array). Setting as true ApplyDbMigrationsOnStartup will apply to every tenant. This is handled at startup by:

```cs
// See: MultiTenantBlazor.Helpers.Middleware.MigrationApplicationBuilder.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ..ommitted code
    app.DbMigrationRunner();
    // ..ommitted code
}

``` 

In summary:

- ApplyDbMigrationsOnStartup = true will apply all migrations to all tenants including localhost
- ApplyDbMigrationsOnStartup = false will apply all migrations to localhost tenant only. You still have the ability to move between tenants and use other Dbs thoughs (assuming the exist and have the required migrations to execute your code).


You can access a different tenant by ammending and adding a subdomain that matches the tenant's name. For example:
``` 
http://tenant1.localhost:42973/
``` 

See Executing to note the required steps you need to do in order to successfully run and access other tenants.

## Executing

To test and execute this locally requires additional steps.

To do this you will need to adjust your host file on your machine to resolve the tenant subdomains. You can either:
- Run updateHosts.bat as administrator within the tools subfolder (Recommended)
- Add this manually like below:
``` 
# localhost name resolution is handled within DNS itself.
#	127.0.0.1       localhost
#	::1             localhost

# Added for multitenant testing
127.0.0.1       tenant1.localhost tenant2.localhost tenant3.localhost
``` 

If you run the updateHosts.bat it will look for this entry: "# Added for MultiTenantBlazor testing"
If it exists it will not update the host file. If it doesn't it will add tenant1, 2 and 3 on a single line.


After this you will need to build the application and go to .vs\MultiTenantBlazor\config\applicationhost.config and adjust the bindings.
``` xml
<bindings>
          <binding protocol="http" bindingInformation="*:42973:localhost" />
          <binding protocol="http" bindingInformation="*:42973:tenant1.localhost" />
          <binding protocol="http" bindingInformation="*:42973:tenant2.localhost" />
          <binding protocol="http" bindingInformation="*:42973:tenant3.localhost" />
          <binding protocol="https" bindingInformation="*:44364:localhost" />
          <binding protocol="https" bindingInformation="*:44364:tenant1.localhost" />
          <binding protocol="https" bindingInformation="*:44364:tenant2.localhost" />
          <binding protocol="https" bindingInformation="*:44364:tenant3.localhost" />
</bindings>
``` 

Once this has added you will get access denied messages within Visual Studio when running the application. You can either:
- Run as administrator (not recommended)
- Run addUrlACLs.bat as administrator within the tools subfolder and restart Visual Studio

There is also deleteUrlACLs.bat if you want to remove the URL ACLs.

## Tech Stack
- Blazor Server

### NuGet Packages : Blazor
- Finbuckle.MultiTenant.AspNetCore
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools