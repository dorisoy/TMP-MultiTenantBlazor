using BlazorTemplates.ServerSide.Helpers.Enums.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplates.ServerSide.Helpers.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params IdentityRoleNames[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(IdentityRoleNames), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
