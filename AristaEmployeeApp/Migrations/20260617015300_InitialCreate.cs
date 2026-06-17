using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AristaEmployeeApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perusahaans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perusahaans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerusahaanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabangs_Perusahaans_PerusahaanId",
                        column: x => x.PerusahaanId,
                        principalTable: "Perusahaans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Karyawans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KodeKaryawan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerusahaanId = table.Column<int>(type: "int", nullable: false),
                    CabangId = table.Column<int>(type: "int", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karyawans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karyawans_Cabangs_CabangId",
                        column: x => x.CabangId,
                        principalTable: "Cabangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Karyawans_Perusahaans_PerusahaanId",
                        column: x => x.PerusahaanId,
                        principalTable: "Perusahaans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cabangs_PerusahaanId",
                table: "Cabangs",
                column: "PerusahaanId");

            migrationBuilder.CreateIndex(
                name: "IX_Karyawans_CabangId",
                table: "Karyawans",
                column: "CabangId");

            migrationBuilder.CreateIndex(
                name: "IX_Karyawans_PerusahaanId",
                table: "Karyawans",
                column: "PerusahaanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Karyawans");

            migrationBuilder.DropTable(
                name: "Cabangs");

            migrationBuilder.DropTable(
                name: "Perusahaans");
        }
    }
}
