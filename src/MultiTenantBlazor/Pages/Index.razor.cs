using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using MultiTenantBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Pages
{
    public partial class Index
    {
        [Inject]
        private ITenantListService _tenantService { get; set; }

        private TenantInfo tenant { get; set; }
        private bool IsReady { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tenant = _tenantService.GetCurrentTenant();
            IsReady = true;
        }
    }
}
