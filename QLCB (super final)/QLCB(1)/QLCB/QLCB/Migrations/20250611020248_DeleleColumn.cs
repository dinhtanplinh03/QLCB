using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class DeleleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_ChuyenBays_ChuyenBayMaChuyenBay",
                table: "PhanCongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_NhanViens_NhanVienMaNhanVien",
                table: "PhanCongs");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_ChungNhans_ChungNhanMaChungNhan",
                table: "VeBays");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_ChuyenBays_ChuyenBayMaChuyenBay",
                table: "VeBays");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_MayBays_MayBayMaMayBay",
                table: "VeBays");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_NhanViens_NhanVienMaNhanVien",
                table: "VeBays");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_PhanCongs_PhanCongId",
                table: "VeBays");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_SanBays_SanBayDenMaSanBay",
                table: "VeBays");

            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_SanBays_SanBayDiMaSanBay",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_ChungNhanMaChungNhan",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_ChuyenBayMaChuyenBay",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_MayBayMaMayBay",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_NhanVienMaNhanVien",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_PhanCongId",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_SanBayDenMaSanBay",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_SanBayDiMaSanBay",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_ChuyenBayMaChuyenBay",
                table: "PhanCongs");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_NhanVienMaNhanVien",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "ChungNhanMaChungNhan",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "ChuyenBayMaChuyenBay",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "MayBayMaMayBay",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNhanVien",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "PhanCongId",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "SanBayDenMaSanBay",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "SanBayDiMaSanBay",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "ChuyenBayMaChuyenBay",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNhanVien",
                table: "PhanCongs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChungNhanMaChungNhan",
                table: "VeBays",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenBayMaChuyenBay",
                table: "VeBays",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MayBayMaMayBay",
                table: "VeBays",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhanVienMaNhanVien",
                table: "VeBays",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhanCongId",
                table: "VeBays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SanBayDenMaSanBay",
                table: "VeBays",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanBayDiMaSanBay",
                table: "VeBays",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenBayMaChuyenBay",
                table: "PhanCongs",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhanVienMaNhanVien",
                table: "PhanCongs",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_ChungNhanMaChungNhan",
                table: "VeBays",
                column: "ChungNhanMaChungNhan");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_ChuyenBayMaChuyenBay",
                table: "VeBays",
                column: "ChuyenBayMaChuyenBay");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_MayBayMaMayBay",
                table: "VeBays",
                column: "MayBayMaMayBay");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_NhanVienMaNhanVien",
                table: "VeBays",
                column: "NhanVienMaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_PhanCongId",
                table: "VeBays",
                column: "PhanCongId");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_SanBayDenMaSanBay",
                table: "VeBays",
                column: "SanBayDenMaSanBay");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_SanBayDiMaSanBay",
                table: "VeBays",
                column: "SanBayDiMaSanBay");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_ChungNhans_ChungNhanMaChungNhan",
                table: "VeBays",
                column: "ChungNhanMaChungNhan",
                principalTable: "ChungNhans",
                principalColumn: "MaChungNhan");

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_ChuyenBays_ChuyenBayMaChuyenBay",
                table: "VeBays",
                column: "ChuyenBayMaChuyenBay",
                principalTable: "ChuyenBays",
                principalColumn: "MaChuyenBay");

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_MayBays_MayBayMaMayBay",
                table: "VeBays",
                column: "MayBayMaMayBay",
                principalTable: "MayBays",
                principalColumn: "MaMayBay");

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_NhanViens_NhanVienMaNhanVien",
                table: "VeBays",
                column: "NhanVienMaNhanVien",
                principalTable: "NhanViens",
                principalColumn: "MaNhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_PhanCongs_PhanCongId",
                table: "VeBays",
                column: "PhanCongId",
                principalTable: "PhanCongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_SanBays_SanBayDenMaSanBay",
                table: "VeBays",
                column: "SanBayDenMaSanBay",
                principalTable: "SanBays",
                principalColumn: "MaSanBay");

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_SanBays_SanBayDiMaSanBay",
                table: "VeBays",
                column: "SanBayDiMaSanBay",
                principalTable: "SanBays",
                principalColumn: "MaSanBay");
        }
    }
}
