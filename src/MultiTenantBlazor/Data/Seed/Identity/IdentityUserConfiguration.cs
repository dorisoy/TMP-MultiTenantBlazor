using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplates.ServerSide.Data.Seed.Identity
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            builder.HasData(
                new IdentityUser
                {
                    Id = "5351c31a-17cd-4740-bf02-cd64bbddc186",
                    UserName = "demo-dev@localhost",
                    NormalizedUserName = "demo-dev@localhost",
                    Email = "demo-dev@localhost",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                },
                new IdentityUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "demo-admin@localhost",
                    NormalizedUserName = "demo-admin@localhost",
                    Email = "demo-admin@localhost",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                },
                new IdentityUser
                {
                    Id = "455f19f2-43db-406b-9f6b-7b4e29da8736",
                    UserName = "demo-user@localhost",
                    NormalizedUserName = "demo-user@localhost",
                    Email = "demo-user@localhost",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                }
            );
        }
    }
}
