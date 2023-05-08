using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class tagModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "741aa845-118a-4ef0-a890-21a7a24b49e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e766218a-0b5e-4eee-aa23-44aa014049b2");

            migrationBuilder.AddColumn<int>(
                name: "TimesUsed",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1212c767-aae7-4389-abb3-8337acd574e8", null, "User", "USER" },
                    { "5bcb3cbe-9ce5-434f-a992-6ee38bb2ab50", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1212c767-aae7-4389-abb3-8337acd574e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bcb3cbe-9ce5-434f-a992-6ee38bb2ab50");

            migrationBuilder.DropColumn(
                name: "TimesUsed",
                table: "Tags");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "741aa845-118a-4ef0-a890-21a7a24b49e2", null, "User", "USER" },
                    { "e766218a-0b5e-4eee-aa23-44aa014049b2", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
