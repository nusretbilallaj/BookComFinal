using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCommerceCustom1.Migrations
{
    public partial class Kompania : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumriITelefonit",
                table: "Kompania",
                newName: "NumriTelefonit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumriTelefonit",
                table: "Kompania",
                newName: "NumriITelefonit");
        }
    }
}
