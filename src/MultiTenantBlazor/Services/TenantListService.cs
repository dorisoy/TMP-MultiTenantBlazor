using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MultiTenantBlazor.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Services
{
    public interface ITenantListService
    {
        List<TenantInfo> GetAllTenants();
        TenantInfo GetCurrentTenant();
    }
    public class TenantListService : ITenantListService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private TenantList _tenantList;
        public TenantListService(IOptionsMonitor<TenantList> tenantList, IHttpContextAccessor httpContextAccessor)
        {
            _tenantList = tenantList.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<TenantInfo> GetAllTenants()
        {
            return _tenantList.Tenants;
        }

        public TenantInfo GetCurrentTenant()
        {
            var tenant = _httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>();
            return tenant.TenantInfo;
        }
    }
}
