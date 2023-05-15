using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recomendations_app.Migrations
{
    /// <inheritdoc />
    public partial class commentmodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24124121-efc5-4dbf-8722-61d30b3f178f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "446d990a-ab9b-441b-9c3e-71f38f5c808c");

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Comments",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "CommentBody" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3fdf8792-efdb-4801-be66-ce4d36e19365", null, "Administrator", "ADMINISTRATOR" },
                    { "7844a669-2935-4026-9109-1792b7e60813", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SearchVector",
                table: "Comments",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_SearchVector",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fdf8792-efdb-4801-be66-ce4d36e19365");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7844a669-2935-4026-9109-1792b7e60813");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24124121-efc5-4dbf-8722-61d30b3f178f", null, "User", "USER" },
                    { "446d990a-ab9b-441b-9c3e-71f38f5c808c", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
