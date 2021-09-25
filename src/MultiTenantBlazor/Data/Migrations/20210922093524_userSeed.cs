using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiTenantBlazor.Data.Migrations
{
    public partial class userSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b0fb1d7-1233-41e9-bfae-7453bc3394b7", "37235e37-4dcc-454c-a07b-1f2c62f52821", "Developer", "DEVELOPER" },
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "d94c2ba3-8d5c-422c-a1b1-f0d5a98e574a", "Administrator", "ADMINISTRATOR" },
                    { "5ebb9344-73fc-449c-b3bc-9aa04018956e", "f8a23e88-c13e-477f-acb7-fd62e59e4853", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5351c31a-17cd-4740-bf02-cd64bbddc186", 0, "0f13292f-2994-48ed-805c-698549566464", "demo-dev@localhost", true, false, null, null, "demo-dev@localhost", "AQAAAAEAACcQAAAAEPikUtkIECg6Y5axtFgjQLDeXrXXD9zmhbP9nCxLFVmRE8VGahoSOgzZXmQ10CTG7Q==", null, false, "19b167c1-09e2-4832-b4a2-ff98f036c0b6", false, "demo-dev@localhost" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "b634948c-295a-45a1-966f-0572f149a89f", "demo-admin@localhost", true, false, null, null, "demo-admin@localhost", "AQAAAAEAACcQAAAAEExAP2eM7M4MTMMws+n80erCsETbpf1prZQ/D7K5Sarr8+9JWP6HyzpCM4eo++VRwg==", null, false, "de5bbb44-2dd9-47ba-9594-872849492fed", false, "demo-admin@localhost" },
                    { "455f19f2-43db-406b-9f6b-7b4e29da8736", 0, "53677374-45a2-40be-8a90-2d89cea75000", "demo-user@localhost", true, false, null, null, "demo-user@localhost", "AQAAAAEAACcQAAAAEN44UQ1N92ntN6TFbp6dbFMsECYMvwmNXUwFgbTjp+16lQbTDstia9HttENTXTLTFQ==", null, false, "424f5c35-49a2-4e90-af65-6cd8fbdecb21", false, "demo-user@localhost" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7b0fb1d7-1233-41e9-bfae-7453bc3394b7", "5351c31a-17cd-4740-bf02-cd64bbddc186" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5ebb9344-73fc-449c-b3bc-9aa04018956e", "455f19f2-43db-406b-9f6b-7b4e29da8736" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5ebb9344-73fc-449c-b3bc-9aa04018956e", "455f19f2-43db-406b-9f6b-7b4e29da8736" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7b0fb1d7-1233-41e9-bfae-7453bc3394b7", "5351c31a-17cd-4740-bf02-cd64bbddc186" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ebb9344-73fc-449c-b3bc-9aa04018956e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b0fb1d7-1233-41e9-bfae-7453bc3394b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "455f19f2-43db-406b-9f6b-7b4e29da8736");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5351c31a-17cd-4740-bf02-cd64bbddc186");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");
        }
    }
}
