using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class tagModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "4a3a19fa-90cd-4eb9-b260-9a49a6602b29", null, "Administrator", "ADMINISTRATOR" },
                    { "c65d4d53-5226-4907-894e-dc32c1d36701", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a3a19fa-90cd-4eb9-b260-9a49a6602b29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c65d4d53-5226-4907-894e-dc32c1d36701");

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
    }
}
