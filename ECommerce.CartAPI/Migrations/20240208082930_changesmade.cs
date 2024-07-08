using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.CartAPI.Migrations
{
    public partial class changesmade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CartHeaders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CartHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
