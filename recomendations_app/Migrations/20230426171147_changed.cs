using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "741aa845-118a-4ef0-a890-21a7a24b49e2", null, "User", "USER" },
                    { "e766218a-0b5e-4eee-aa23-44aa014049b2", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "741aa845-118a-4ef0-a890-21a7a24b49e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e766218a-0b5e-4eee-aa23-44aa014049b2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8412080c-34e8-4b6c-97e2-51bf274d2d1a", null, "Administrator", "ADMINISTRATOR" },
                    { "eba62f0e-7d31-4342-9c77-71f271da7b45", null, "User", "USER" }
                });
        }
    }
}
