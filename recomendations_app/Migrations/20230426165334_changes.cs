using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04129ced-6613-4d57-af43-4c11a270745e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e5dd6d6-16b1-4192-a6e0-726dfbf1e84a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8412080c-34e8-4b6c-97e2-51bf274d2d1a", null, "Administrator", "ADMINISTRATOR" },
                    { "eba62f0e-7d31-4342-9c77-71f271da7b45", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8412080c-34e8-4b6c-97e2-51bf274d2d1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eba62f0e-7d31-4342-9c77-71f271da7b45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04129ced-6613-4d57-af43-4c11a270745e", null, "User", "USER" },
                    { "0e5dd6d6-16b1-4192-a6e0-726dfbf1e84a", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
