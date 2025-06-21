using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class updatePhanCong1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChuyenBayMaChuyenBay",
                table: "PhanCongs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhanVienMaNhanVien",
                table: "PhanCongs",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_ChuyenBayMaChuyenBay",
                table: "PhanCongs",
                column: "ChuyenBayMaChuyenBay");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_NhanVienMaNhanVien",
                table: "PhanCongs",
                column: "NhanVienMaNhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_ChuyenBays_ChuyenBayMaChuyenBay",
                table: "PhanCongs",
                column: "ChuyenBayMaChuyenBay",
                principalTable: "ChuyenBays",
                principalColumn: "MaChuyenBay");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_NhanViens_NhanVienMaNhanVien",
                table: "PhanCongs",
                column: "NhanVienMaNhanVien",
                principalTable: "NhanViens",
                principalColumn: "MaNhanVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_ChuyenBays_ChuyenBayMaChuyenBay",
                table: "PhanCongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_NhanViens_NhanVienMaNhanVien",
                table: "PhanCongs");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_ChuyenBayMaChuyenBay",
                table: "PhanCongs");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_NhanVienMaNhanVien",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "ChuyenBayMaChuyenBay",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNhanVien",
                table: "PhanCongs");
        }
    }
}
