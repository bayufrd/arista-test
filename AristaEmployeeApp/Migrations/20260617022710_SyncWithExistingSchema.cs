using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AristaEmployeeApp.Migrations
{
    /// <inheritdoc />
    public partial class SyncWithExistingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabangs_Perusahaans_PerusahaanId",
                table: "Cabangs");

            migrationBuilder.DropForeignKey(
                name: "FK_Karyawans_Cabangs_CabangId",
                table: "Karyawans");

            migrationBuilder.DropForeignKey(
                name: "FK_Karyawans_Perusahaans_PerusahaanId",
                table: "Karyawans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perusahaans",
                table: "Perusahaans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Karyawans",
                table: "Karyawans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cabangs",
                table: "Cabangs");

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

            migrationBuilder.RenameTable(
                name: "Perusahaans",
                newName: "Perusahaan");

            migrationBuilder.RenameTable(
                name: "Karyawans",
                newName: "Karyawan");

            migrationBuilder.RenameTable(
                name: "Cabangs",
                newName: "Cabang");

            migrationBuilder.RenameColumn(
                name: "Nama",
                table: "Perusahaan",
                newName: "NamaPerusahaan");

            migrationBuilder.RenameColumn(
                name: "Nama",
                table: "Karyawan",
                newName: "NamaKaryawan");

            migrationBuilder.RenameIndex(
                name: "IX_Karyawans_PerusahaanId",
                table: "Karyawan",
                newName: "IX_Karyawan_PerusahaanId");

            migrationBuilder.RenameIndex(
                name: "IX_Karyawans_CabangId",
                table: "Karyawan",
                newName: "IX_Karyawan_CabangId");

            migrationBuilder.RenameColumn(
                name: "Nama",
                table: "Cabang",
                newName: "NamaCabang");

            migrationBuilder.RenameIndex(
                name: "IX_Cabangs_PerusahaanId",
                table: "Cabang",
                newName: "IX_Cabang_PerusahaanId");

            migrationBuilder.AddColumn<bool>(
                name: "Aktif",
                table: "Perusahaan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Aktif",
                table: "Cabang",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perusahaan",
                table: "Perusahaan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Karyawan",
                table: "Karyawan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cabang",
                table: "Cabang",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabang_Perusahaan_PerusahaanId",
                table: "Cabang",
                column: "PerusahaanId",
                principalTable: "Perusahaan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Karyawan_Cabang_CabangId",
                table: "Karyawan",
                column: "CabangId",
                principalTable: "Cabang",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Karyawan_Perusahaan_PerusahaanId",
                table: "Karyawan",
                column: "PerusahaanId",
                principalTable: "Perusahaan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabang_Perusahaan_PerusahaanId",
                table: "Cabang");

            migrationBuilder.DropForeignKey(
                name: "FK_Karyawan_Cabang_CabangId",
                table: "Karyawan");

            migrationBuilder.DropForeignKey(
                name: "FK_Karyawan_Perusahaan_PerusahaanId",
                table: "Karyawan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perusahaan",
                table: "Perusahaan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Karyawan",
                table: "Karyawan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cabang",
                table: "Cabang");

            migrationBuilder.DropColumn(
                name: "Aktif",
                table: "Perusahaan");

            migrationBuilder.DropColumn(
                name: "Aktif",
                table: "Cabang");

            migrationBuilder.RenameTable(
                name: "Perusahaan",
                newName: "Perusahaans");

            migrationBuilder.RenameTable(
                name: "Karyawan",
                newName: "Karyawans");

            migrationBuilder.RenameTable(
                name: "Cabang",
                newName: "Cabangs");

            migrationBuilder.RenameColumn(
                name: "NamaPerusahaan",
                table: "Perusahaans",
                newName: "Nama");

            migrationBuilder.RenameColumn(
                name: "NamaKaryawan",
                table: "Karyawans",
                newName: "Nama");

            migrationBuilder.RenameIndex(
                name: "IX_Karyawan_PerusahaanId",
                table: "Karyawans",
                newName: "IX_Karyawans_PerusahaanId");

            migrationBuilder.RenameIndex(
                name: "IX_Karyawan_CabangId",
                table: "Karyawans",
                newName: "IX_Karyawans_CabangId");

            migrationBuilder.RenameColumn(
                name: "NamaCabang",
                table: "Cabangs",
                newName: "Nama");

            migrationBuilder.RenameIndex(
                name: "IX_Cabang_PerusahaanId",
                table: "Cabangs",
                newName: "IX_Cabangs_PerusahaanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perusahaans",
                table: "Perusahaans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Karyawans",
                table: "Karyawans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cabangs",
                table: "Cabangs",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cabangs_Perusahaans_PerusahaanId",
                table: "Cabangs",
                column: "PerusahaanId",
                principalTable: "Perusahaans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Karyawans_Cabangs_CabangId",
                table: "Karyawans",
                column: "CabangId",
                principalTable: "Cabangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Karyawans_Perusahaans_PerusahaanId",
                table: "Karyawans",
                column: "PerusahaanId",
                principalTable: "Perusahaans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
