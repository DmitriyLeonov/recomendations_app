using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recomendations_app.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "560fe37d-f3d1-45f6-b707-6e6c6dc38923", "585a0f69-82f6-4337-9bca-c6424ed4a023", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2aa523f-340b-4a0a-8ec2-aac5860ea8e1", "756914d7-eed0-4c96-8ee5-db2861195a50", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "560fe37d-f3d1-45f6-b707-6e6c6dc38923");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2aa523f-340b-4a0a-8ec2-aac5860ea8e1");
        }
    }
}
