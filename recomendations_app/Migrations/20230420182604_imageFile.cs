using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class imageFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41562c33-bb9c-45f8-a620-f972f828ab23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50916680-3b9c-4f51-872a-4204f6493a4f");

            migrationBuilder.AlterColumn<string>(
                name: "ImageStorageName",
                table: "Reviews",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7348d976-5f44-4a83-8afa-fa9f4b68f365", null, "User", "USER" },
                    { "e2079ab8-599d-4db9-a36c-a370baf3b011", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7348d976-5f44-4a83-8afa-fa9f4b68f365");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2079ab8-599d-4db9-a36c-a370baf3b011");

            migrationBuilder.AlterColumn<long>(
                name: "ImageStorageName",
                table: "Reviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41562c33-bb9c-45f8-a620-f972f828ab23", null, "Administrator", "ADMINISTRATOR" },
                    { "50916680-3b9c-4f51-872a-4204f6493a4f", null, "User", "USER" }
                });
        }
    }
}
