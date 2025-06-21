using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVeBay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeBays_HanhKhachs_HanhKhachMaHanhKhach",
                table: "VeBays");

            migrationBuilder.DropIndex(
                name: "IX_VeBays_HanhKhachMaHanhKhach",
                table: "VeBays");

            migrationBuilder.DropColumn(
                name: "HanhKhachMaHanhKhach",
                table: "VeBays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HanhKhachMaHanhKhach",
                table: "VeBays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VeBays_HanhKhachMaHanhKhach",
                table: "VeBays",
                column: "HanhKhachMaHanhKhach");

            migrationBuilder.AddForeignKey(
                name: "FK_VeBays_HanhKhachs_HanhKhachMaHanhKhach",
                table: "VeBays",
                column: "HanhKhachMaHanhKhach",
                principalTable: "HanhKhachs",
                principalColumn: "MaHanhKhach",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
