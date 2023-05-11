using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class imageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "404b7c93-c7d8-4d43-a951-77aa2d586a11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b20c0f2-4aff-4ecc-b503-2fb50bd87b7d");

            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ImageStorageName",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageLink = table.Column<string>(type: "text", nullable: true),
                    ImageStorageName = table.Column<string>(type: "text", nullable: true),
                    ReviewId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4894dc20-8da7-491e-adda-16786116f995", null, "Administrator", "ADMINISTRATOR" },
                    { "8a9584d6-f59f-44f7-a7fc-cec757f13f5f", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ReviewId",
                table: "Images",
                column: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4894dc20-8da7-491e-adda-16786116f995");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a9584d6-f59f-44f7-a7fc-cec757f13f5f");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageStorageName",
                table: "Reviews",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "404b7c93-c7d8-4d43-a951-77aa2d586a11", null, "User", "USER" },
                    { "4b20c0f2-4aff-4ecc-b503-2fb50bd87b7d", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
