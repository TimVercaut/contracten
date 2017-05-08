using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class Contracten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    ConsultantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Familienaam = table.Column<string>(nullable: false),
                    Soort = table.Column<int>(nullable: false),
                    Telefoon = table.Column<string>(nullable: true),
                    Voornaam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.ConsultantID);
                });

            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    KlantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Contactpersoon = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gemeente = table.Column<string>(nullable: true),
                    Huisnummer = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: true),
                    Straat = table.Column<string>(nullable: true),
                    Telefoon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.KlantID);
                });

            migrationBuilder.CreateTable(
                name: "Contracten",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsultantID = table.Column<int>(nullable: false),
                    EindDatum = table.Column<DateTime>(type: "date", nullable: false),
                    KlantID = table.Column<int>(nullable: false),
                    Kosten = table.Column<decimal>(nullable: true),
                    OnderKlant = table.Column<int>(nullable: true),
                    Opzegtermijn = table.Column<string>(nullable: true),
                    Randvoorwaarden = table.Column<string>(nullable: true),
                    StartDatum = table.Column<DateTime>(type: "date", nullable: false),
                    Tarief = table.Column<decimal>(nullable: false),
                    Verlenging = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracten", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contracten_Consultants_ConsultantID",
                        column: x => x.ConsultantID,
                        principalTable: "Consultants",
                        principalColumn: "ConsultantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracten_Klanten_KlantID",
                        column: x => x.KlantID,
                        principalTable: "Klanten",
                        principalColumn: "KlantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracten_ConsultantID",
                table: "Contracten",
                column: "ConsultantID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracten_KlantID",
                table: "Contracten",
                column: "KlantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracten");

            migrationBuilder.DropTable(
                name: "Consultants");

            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
