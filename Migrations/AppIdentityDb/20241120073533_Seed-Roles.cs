using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnStudentAPI.Migrations.AppIdentityDb
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15ac2a4c-4c9d-4407-8422-f5d8f243e355", "2", "User", "USER" },
                    { "9c4af0dd-c909-4c0f-abe9-0373566173ab", "3", "AddStudentRole", "ADDSTUDENTROLE" },
                    { "e54045ed-2688-4ede-a0c2-2b2b7135547c", "1", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15ac2a4c-4c9d-4407-8422-f5d8f243e355");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c4af0dd-c909-4c0f-abe9-0373566173ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e54045ed-2688-4ede-a0c2-2b2b7135547c");
        }
    }
}
