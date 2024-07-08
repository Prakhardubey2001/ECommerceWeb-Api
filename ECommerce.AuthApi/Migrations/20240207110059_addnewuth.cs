using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.AuthApi.Migrations
{
    public partial class addnewuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "027e6dfe-162c-4ca7-8804-17bb858fa405");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24cd924b-3fba-482a-af14-25252a807027");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44fb436f-8cc0-4d86-9247-9d83f0b5b106");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bf5e85e5-5f5d-4b79-8e4c-090ddd1ba653");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8346af09-9a85-4adb-a668-d742d4dacdf8", 0, "655abbf7-55fe-4ad5-9e7b-179ddd7ddb09", "admin02@gmail.com", true, false, null, "admin02@gmail.com", null, "AQAAAAEAACcQAAAAEA0+u5Is1Qr4JsvmJKljG/hG8RROn8VV4b+iVr3U/7W5oo5wg1oitA2KYCwf7dqSnA==", null, false, "Admin", "8346af09-9a85-4adb-a668-d742d4dacdf8", false, "admin02@gmail.com" },
                    { "85284e05-69d4-4aca-baa8-9cb41c926826", 0, "66de1587-b890-4947-8c17-a666a0469d8f", "user02@gmail.com", true, false, null, "user02@gmail.com", null, "AQAAAAEAACcQAAAAECSVlHiOsvm7LMr+UpUIyzwqSkVDgzh189WfLTHnAKyw4SHZtyk/89JiA5HhbuhU9g==", null, false, "User", "85284e05-69d4-4aca-baa8-9cb41c926826", false, "user02@gmail.com" },
                    { "b52bbf7e-4b84-42b2-8191-d68c3855ca50", 0, "47547c7d-e7d7-4d87-9d58-72c24bb07e68", "user01@gmail.com", true, false, null, "user01@gmail.com", null, "AQAAAAEAACcQAAAAEI1uCy0a/VVXA8Fq1axW7jIb+IDbh8fwVTKcYCjImpYlkKaR1SsPlg96Bd56lwvVFQ==", null, false, "User", "b52bbf7e-4b84-42b2-8191-d68c3855ca50", false, "user01@gmail.com" },
                    { "dfabbed9-159e-4f65-9e15-f7558105cc83", 0, "40668b23-df7b-4b60-89a5-46b676a123aa", "admin01@gmail.com", true, false, null, "admin01@gmail.com", null, "AQAAAAEAACcQAAAAEJ5QjJ+q1teNoSaduMZBlSIAwBvtcQxa5PWkHl8dlqpRm6ZUv/iaLFneD7Kht4zkwg==", null, false, "Admin", "dfabbed9-159e-4f65-9e15-f7558105cc83", false, "admin01@gmail.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8346af09-9a85-4adb-a668-d742d4dacdf8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85284e05-69d4-4aca-baa8-9cb41c926826");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b52bbf7e-4b84-42b2-8191-d68c3855ca50");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dfabbed9-159e-4f65-9e15-f7558105cc83");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "027e6dfe-162c-4ca7-8804-17bb858fa405", 0, "2b9cb2e4-a08a-47d3-aa89-497ca1d85379", "user01@gmail.com", true, false, null, "user01@gmail.com", null, "AQAAAAEAACcQAAAAECy4N+38OHfMjIS/JfdAgndctGcNGb9YfoWfZguIlnmdvUscZya3BS1u73EDLZzzZQ==", null, false, "User", "027e6dfe-162c-4ca7-8804-17bb858fa405", false, "user01@gmail.com" },
                    { "24cd924b-3fba-482a-af14-25252a807027", 0, "82d8dcde-5505-4d29-b2bb-9c5e5e29f11c", "user02@gmail.com", true, false, null, "user02@gmail.com", null, "AQAAAAEAACcQAAAAEPUhbh1BIXsnXBRsP1f9+0l7k7dzpjySi6zGQgOuBZUheE09WosyfHo0Py9cddWKpw==", null, false, "User", "24cd924b-3fba-482a-af14-25252a807027", false, "user02@gmail.com" },
                    { "44fb436f-8cc0-4d86-9247-9d83f0b5b106", 0, "9dd21843-b8f8-4652-b517-bcfafd2cfe6d", "admin02@gmail.com", true, false, null, "admin02@gmail.com", null, "AQAAAAEAACcQAAAAEDOv66jfJbTlFKVP+lJHKwX/FgsYtcm+8Niqv3w5RBTCmjMU9CzWFE0gq8NJa6Ffkw==", null, false, "Admin", "44fb436f-8cc0-4d86-9247-9d83f0b5b106", false, "admin02@gmail.com" },
                    { "bf5e85e5-5f5d-4b79-8e4c-090ddd1ba653", 0, "d6869287-cdf6-4aa1-be0d-0be33d667e46", "admin01@gmail.com", true, false, null, "admin01@gmail.com", null, "AQAAAAEAACcQAAAAELSDMpTZekEK8wIpW05w80a+WJ6L8U+2wTsC7rmpAzB8IlXsRHs42aCKAwQMFpDl3Q==", null, false, "Admin", "bf5e85e5-5f5d-4b79-8e4c-090ddd1ba653", false, "admin01@gmail.com" }
                });
        }
    }
}
