using Finbuckle.MultiTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Models.Configuration
{
    public class TenantList
    {
        public List<TenantInfo> Tenants { get; set; }
    }
}
