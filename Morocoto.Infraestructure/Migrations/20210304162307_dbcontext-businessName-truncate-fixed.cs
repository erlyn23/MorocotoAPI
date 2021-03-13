using Microsoft.EntityFrameworkCore.Migrations;

namespace Morocoto.Infraestructure.Migrations
{
    public partial class DbcontextbusinessNametruncatefixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "Business",
                table: "Businesses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Business",
                table: "Businesses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

        }
    }
}
