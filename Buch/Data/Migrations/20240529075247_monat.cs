using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buch.Data.Migrations
{
    public partial class monat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Monat",
                table: "Kassenbuch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monat",
                table: "Kassenbuch");
        }
    }
}
