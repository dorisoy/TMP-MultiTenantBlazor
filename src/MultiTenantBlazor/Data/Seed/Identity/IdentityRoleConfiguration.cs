using BlazorTemplates.ServerSide.Helpers.Enums.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplates.ServerSide.Data.Seed.Identity
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "7b0fb1d7-1233-41e9-bfae-7453bc3394b7",
                    Name = IdentityRoleNames.Developer.ToString(),
                    NormalizedName = IdentityRoleNames.Developer.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = IdentityRoleNames.Administrator.ToString(),
                    NormalizedName = IdentityRoleNames.Administrator.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = "5ebb9344-73fc-449c-b3bc-9aa04018956e",
                    Name = IdentityRoleNames.User.ToString(),
                    NormalizedName = IdentityRoleNames.User.ToString().ToUpper()
                }
            );
        }
    }
}
