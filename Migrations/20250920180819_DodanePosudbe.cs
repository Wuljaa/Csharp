using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmovizija.Migrations
{
    /// <inheritdoc />
    public partial class DodanePosudbe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posudbe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KorisnikId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false),
                    DatumPosudbe = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DatumPovratka = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Vraceno = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posudbe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posudbe_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posudbe_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posudbe_FilmId",
                table: "Posudbe",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Posudbe_KorisnikId",
                table: "Posudbe",
                column: "KorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posudbe");
        }
    }
}
