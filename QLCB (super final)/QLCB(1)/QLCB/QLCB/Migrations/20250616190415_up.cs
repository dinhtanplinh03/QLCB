using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThoiTiet",
                table: "ChuyenBays",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "ChuyenBays",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiTiet",
                table: "ChuyenBays");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "ChuyenBays");
        }
    }
}
