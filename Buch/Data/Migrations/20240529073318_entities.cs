using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buch.Data.Migrations
{
    public partial class entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kassenbuch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anfangsbestand = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kassenbuch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Farbe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rechnung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Betrag = table.Column<double>(type: "float", nullable: false),
                    Art = table.Column<int>(type: "int", nullable: false),
                    KassenbuchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rechnung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rechnung_Kassenbuch_KassenbuchId",
                        column: x => x.KassenbuchId,
                        principalTable: "Kassenbuch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RechnungKategorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategorieId = table.Column<int>(type: "int", nullable: false),
                    RechnungId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechnungKategorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RechnungKategorie_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RechnungKategorie_Rechnung_RechnungId",
                        column: x => x.RechnungId,
                        principalTable: "Rechnung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rechnung_KassenbuchId",
                table: "Rechnung",
                column: "KassenbuchId");

            migrationBuilder.CreateIndex(
                name: "IX_RechnungKategorie_KategorieId",
                table: "RechnungKategorie",
                column: "KategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_RechnungKategorie_RechnungId",
                table: "RechnungKategorie",
                column: "RechnungId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RechnungKategorie");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Rechnung");

            migrationBuilder.DropTable(
                name: "Kassenbuch");
        }
    }
}
