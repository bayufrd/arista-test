using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AristaEmployeeApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Perusahaans",
                columns: new[] { "Id", "Nama" },
                values: new object[,]
                {
                    { 1, "Arista Group" },
                    { 2, "Test Corp" }
                });

            migrationBuilder.InsertData(
                table: "Cabangs",
                columns: new[] { "Id", "Nama", "PerusahaanId" },
                values: new object[,]
                {
                    { 1, "Jakarta", 1 },
                    { 2, "Bandung", 1 },
                    { 3, "Surabaya", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cabangs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cabangs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cabangs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Perusahaans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Perusahaans",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
