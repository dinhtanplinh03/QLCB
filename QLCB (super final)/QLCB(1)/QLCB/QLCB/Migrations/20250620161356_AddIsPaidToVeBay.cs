using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCB.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPaidToVeBay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "VeBays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "VeBays");
        }
    }
}
