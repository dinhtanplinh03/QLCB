using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HanhKhachs",
                columns: table => new
                {
                    MaHanhKhach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CMND = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HanhKhachs", x => x.MaHanhKhach);
                });

            migrationBuilder.CreateTable(
                name: "MayBays",
                columns: table => new
                {
                    MaMayBay = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    LoaiMayBay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TamBay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MayBays", x => x.MaMayBay);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNhanVien = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Luong = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "SanBays",
                columns: table => new
                {
                    MaSanBay = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TenSanBay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QuocGia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanBays", x => x.MaSanBay);
                });

            migrationBuilder.CreateTable(
                name: "ChungNhans",
                columns: table => new
                {
                    MaChungNhan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenChungNhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienMaNhanVien = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChungNhans", x => x.MaChungNhan);
                    table.ForeignKey(
                        name: "FK_ChungNhans_NhanViens_NhanVienMaNhanVien",
                        column: x => x.NhanVienMaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChuyenBays",
                columns: table => new
                {
                    MaChuyenBay = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TenChuyenBay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThoiGianKhoiHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianDen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoGhe = table.Column<int>(type: "int", nullable: false),
                    MaMayBay = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MaSanBayDi = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MaSanBayDen = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenBays", x => x.MaChuyenBay);
                    table.ForeignKey(
                        name: "FK_ChuyenBays_MayBays_MaMayBay",
                        column: x => x.MaMayBay,
                        principalTable: "MayBays",
                        principalColumn: "MaMayBay",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChuyenBays_SanBays_MaSanBayDen",
                        column: x => x.MaSanBayDen,
                        principalTable: "SanBays",
                        principalColumn: "MaSanBay",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChuyenBays_SanBays_MaSanBayDi",
                        column: x => x.MaSanBayDi,
                        principalTable: "SanBays",
                        principalColumn: "MaSanBay",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhanCongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChuyenBay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuyenBayMaChuyenBay = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienMaNhanVien = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    NgayPhanCong = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanCongs_ChuyenBays_ChuyenBayMaChuyenBay",
                        column: x => x.ChuyenBayMaChuyenBay,
                        principalTable: "ChuyenBays",
                        principalColumn: "MaChuyenBay");
                    table.ForeignKey(
                        name: "FK_PhanCongs_NhanViens_NhanVienMaNhanVien",
                        column: x => x.NhanVienMaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "VeBays",
                columns: table => new
                {
                    MaVeBay = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaChuyenBay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHanhKhach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayDatVe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiaVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChuyenBayMaChuyenBay = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    HanhKhachMaHanhKhach = table.Column<int>(type: "int", nullable: false),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienMaNhanVien = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MaSanBayDi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanBayDiMaSanBay = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MaSanBayDen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanBayDenMaSanBay = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MaMayBay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MayBayMaMayBay = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MaChungNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChungNhanMaChungNhan = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    MaPhanCong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhanCongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeBays", x => x.MaVeBay);
                    table.ForeignKey(
                        name: "FK_VeBays_ChungNhans_ChungNhanMaChungNhan",
                        column: x => x.ChungNhanMaChungNhan,
                        principalTable: "ChungNhans",
                        principalColumn: "MaChungNhan");
                    table.ForeignKey(
                        name: "FK_VeBays_ChuyenBays_ChuyenBayMaChuyenBay",
                        column: x => x.ChuyenBayMaChuyenBay,
                        principalTable: "ChuyenBays",
                        principalColumn: "MaChuyenBay");
                    table.ForeignKey(
                        name: "FK_VeBays_HanhKhachs_HanhKhachMaHanhKhach",
                        column: x => x.HanhKhachMaHanhKhach,
                        principalTable: "HanhKhachs",
                        principalColumn: "MaHanhKhach",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeBays_MayBays_MayBayMaMayBay",
                        column: x => x.MayBayMaMayBay,
                        principalTable: "MayBays",
                        principalColumn: "MaMayBay");
                    table.ForeignKey(
                        name: "FK_VeBays_NhanViens_NhanVienMaNhanVien",
                        column: x => x.NhanVienMaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                    table.ForeignKey(
                        name: "FK_VeBays_PhanCongs_PhanCongId",
                        column: x => x.PhanCongId,
                        principalTable: "PhanCongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeBays_SanBays_SanBayDenMaSanBay",
                        column: x => x.SanBayDenMaSanBay,
                        principalTable: "SanBays",
                        principalColumn: "MaSanBay");
                    table.ForeignKey(
                        name: "FK_VeBays_SanBays_SanBayDiMaSanBay",
                        column: x => x.SanBayDiMaSanBay,
                        principalTable: "SanBays",
                        principalColumn: "MaSanBay");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChungNhans_NhanVienMaNhanVien",
                table: "ChungNhans",
                column: "NhanVienMaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenBays_MaMayBay",
                table: "ChuyenBays",
                column: "MaMayBay");

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenBays_MaSanBayDen",
                table: "ChuyenBays",
                column: "MaSanBayDen");

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenBays_MaSanBayDi",
                table: "ChuyenBays",
                column: "MaSanBayDi");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_ChuyenBayMaChuyenBay",
                table: "PhanCongs",
                column: "ChuyenBayMaChuyenBay");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_NhanVienMaNhanVien",
                table: "PhanCongs",
                column: "NhanVienMaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_ChungNhanMaChungNhan",
                table: "VeBays",
                column: "ChungNhanMaChungNhan");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_ChuyenBayMaChuyenBay",
                table: "VeBays",
                column: "ChuyenBayMaChuyenBay");

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_HanhKhachMaHanhKhach",
                table: "VeBays",
                column: "HanhKhachMaHanhKhach");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeBays");

            migrationBuilder.DropTable(
                name: "ChungNhans");

            migrationBuilder.DropTable(
                name: "HanhKhachs");

            migrationBuilder.DropTable(
                name: "PhanCongs");

            migrationBuilder.DropTable(
                name: "ChuyenBays");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "MayBays");

            migrationBuilder.DropTable(
                name: "SanBays");
        }
    }
}
