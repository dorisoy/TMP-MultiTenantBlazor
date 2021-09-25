using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantBlazor.Shared
{
    public partial class NavMenu
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        private bool collapseNavMenu = true;

        private string Name { get; set; }

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState;
            Name = authState.User.Identity.Name;
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
