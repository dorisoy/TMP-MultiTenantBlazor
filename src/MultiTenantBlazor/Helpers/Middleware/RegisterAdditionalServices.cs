using MultiTenantBlazor.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Helpers.Middleware
{
    public static class RegisterAdditionalServices
    {
        public static IServiceCollection RegisterApplicationSpecificServices(this IServiceCollection services, IConfiguration config)
        {
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("MultiTenantBlazor"))
                .Where(x => x.Name.EndsWith("Repository") ||
                            x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}
