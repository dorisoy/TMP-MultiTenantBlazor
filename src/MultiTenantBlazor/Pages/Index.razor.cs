using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Pages
{
    public partial class Index
    {
        [Inject]
        private IHttpContextAccessor httpContextAccessor { get; set; }

        private TenantInfo tenant { get; set; }
        private bool IsReady { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var mtInfo = httpContextAccessor.HttpContext.GetMultiTenantContext<TenantInfo>();
            tenant = mtInfo?.TenantInfo;
            IsReady = true;
        }
    }
}
