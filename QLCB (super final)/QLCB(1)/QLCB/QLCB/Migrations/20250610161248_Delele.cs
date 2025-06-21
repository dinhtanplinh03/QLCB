using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class Delele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChungNhans_NhanViens_NhanVienMaNhanVien",
                table: "ChungNhans");

            migrationBuilder.DropIndex(
                name: "IX_ChungNhans_NhanVienMaNhanVien",
                table: "ChungNhans");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNhanVien",
                table: "ChungNhans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NhanVienMaNhanVien",
                table: "ChungNhans",
                type: "nvarchar(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChungNhans_NhanVienMaNhanVien",
                table: "ChungNhans",
                column: "NhanVienMaNhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_ChungNhans_NhanViens_NhanVienMaNhanVien",
                table: "ChungNhans",
                column: "NhanVienMaNhanVien",
                principalTable: "NhanViens",
                principalColumn: "MaNhanVien",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
