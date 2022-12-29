using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCommerceCustom1.Migrations
{
    public partial class Shtimiiporosise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Porosia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerdorusiId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataEPorosise = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEDergeses = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Totali = table.Column<double>(type: "float", nullable: false),
                    StatusiIPorosise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusiIPageses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Posta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumriGjurmues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEPageses = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPerPagese = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumriITelefonit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rruga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qyteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodiPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porosia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porosia_AspNetUsers_PerdorusiId",
                        column: x => x.PerdorusiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PorosiDetalet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PorosiaId = table.Column<int>(type: "int", nullable: false),
                    ProduktiId = table.Column<int>(type: "int", nullable: false),
                    Sasia = table.Column<double>(type: "float", nullable: false),
                    Cmimi = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PorosiDetalet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PorosiDetalet_Porosia_PorosiaId",
                        column: x => x.PorosiaId,
                        principalTable: "Porosia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PorosiDetalet_Produkti_ProduktiId",
                        column: x => x.ProduktiId,
                        principalTable: "Produkti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Porosia_PerdorusiId",
                table: "Porosia",
                column: "PerdorusiId");

            migrationBuilder.CreateIndex(
                name: "IX_PorosiDetalet_PorosiaId",
                table: "PorosiDetalet",
                column: "PorosiaId");

            migrationBuilder.CreateIndex(
                name: "IX_PorosiDetalet_ProduktiId",
                table: "PorosiDetalet",
                column: "ProduktiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PorosiDetalet");

            migrationBuilder.DropTable(
                name: "Porosia");
        }
    }
}
