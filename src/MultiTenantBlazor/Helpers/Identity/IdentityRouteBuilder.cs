using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplates.ServerSide.Helpers.Identity
{
    public static class IdentityRouteBuilder
    {
        public static IEndpointRouteBuilder MapIdentityRedirects(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true)));
            endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true)));

            endpoints.MapGet("/Identity/Account/Manage", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Manage/ChangePassword", true)));
            endpoints.MapPost("/Identity/Account/Manage", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Manage/ChangePassword", true)));

            endpoints.MapGet("/Identity/Account/Manage/Email", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Manage/ChangePassword", true)));
            endpoints.MapPost("/Identity/Account/Manage/Email", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Manage/ChangePassword", true)));

            endpoints.MapGet("/Identity/Account/Manage/PersonalData", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Manage/ChangePassword", true)));
            endpoints.MapPost("/Identity/Account/Manage/PersonalData", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Manage/ChangePassword", true)));

            return endpoints;
        }
    }
}
