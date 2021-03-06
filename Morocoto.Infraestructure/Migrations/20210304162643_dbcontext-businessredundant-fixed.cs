using Microsoft.EntityFrameworkCore.Migrations;

namespace Morocoto.Infraestructure.Migrations
{
    public partial class dbcontextbusinessredundantfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Business",
                table: "Businesses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Business",
                table: "Businesses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);
        }
    }
}
