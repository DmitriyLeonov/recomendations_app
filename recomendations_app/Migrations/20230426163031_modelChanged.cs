using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class modelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7348d976-5f44-4a83-8afa-fa9f4b68f365");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2079ab8-599d-4db9-a36c-a370baf3b011");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04129ced-6613-4d57-af43-4c11a270745e", null, "User", "USER" },
                    { "0e5dd6d6-16b1-4192-a6e0-726dfbf1e84a", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "7348d976-5f44-4a83-8afa-fa9f4b68f365", null, "User", "USER" },
                    { "e2079ab8-599d-4db9-a36c-a370baf3b011", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
