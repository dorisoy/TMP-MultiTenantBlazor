# MultiTenantBlazor

Contains an example Blazor Server application that is multi-tenanted with data isolation per tenant.

## Description

The solution is split a single project within the src folder:
- MultiTenantBlazor

It uses the Finbuckle.MultiTenant.AspNetCore NuGet package for most of the tenant configuration but also supports EF Core tooling* and migrations*.
Tenants are defined in appsettings under "Finbuckle:MultiTenant:Stores:ConfigurationStore". There is an array of "Tenants" which contains the id, identifier, name and connection string of its DB. The first tenant within the array must be kept as localhost as it is used in development and when you need to us EF Core tooling to create migrations. The ForceLocalOnlyDbConnection value will force only the localhost (1st tenant) Db connection. This is so you can use the previously mentioned tooling otherwise you will get null reference exceptions. It's also used for continuous development as you can test with the localhost tenant first before pushing out any changes. To push out the migrations across all tenants as well as each tenant having their own DB connection change ForceLocalOnlyDbConnection to false. This is handled at startup by:

```
// See: MultiTenantBlazor.Helpers.Middleware.MigrationApplicationBuilder.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ..ommitted code
    app.DbMigrationRunner();
    // ..ommitted code
}

``` 

In summary:

- ForceLocalOnlyDbConnection = true in local testing where multitenant connections aren't required
- ForceLocalOnlyDbConnection = false in production or testing different tenant DBs


You can access a different tenant by ammending and adding a subdomain that matches the tenant's name. For example:
``` 
http://tenant1.localhost:42973/
``` 

See Executing to note the required steps you need to do in order to successfully run and access other tenants.

## Executing

To test and execute this locally requires additional steps.

To do this you will need to adjust your host file on your machine to resolve the subdomains:
``` 
# localhost name resolution is handled within DNS itself.
#	127.0.0.1       localhost
#	::1             localhost

# Added for multitenant testing
127.0.0.1       tenant1.localhost tenant2.localhost tenant3.localhost
``` 

After this you will need to build the application and go to .vs\MultiTenantBlazor\config\applicationhost.config and adjust the bindings.
``` 
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



