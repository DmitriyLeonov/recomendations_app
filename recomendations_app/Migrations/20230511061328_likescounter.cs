using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class likescounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "338beb3f-ef84-4e88-b75f-b4221cec4033");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edc8aacb-f45b-4779-8989-07bc32b081fa");

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a2f366e-df44-47be-816e-f7fadcb0980a", null, "Administrator", "ADMINISTRATOR" },
                    { "702c709d-b6d0-4873-8729-8f60d0000f0e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a2f366e-df44-47be-816e-f7fadcb0980a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "702c709d-b6d0-4873-8729-8f60d0000f0e");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "338beb3f-ef84-4e88-b75f-b4221cec4033", null, "User", "USER" },
                    { "edc8aacb-f45b-4779-8989-07bc32b081fa", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
