using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class Archief : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oudecontracten");

            migrationBuilder.AddColumn<int>(
                name: "ContractArchiefId",
                table: "Contracten",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractenArchief",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Jaar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractenArchief", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracten_ContractArchiefId",
                table: "Contracten",
                column: "ContractArchiefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracten_ContractenArchief_ContractArchiefId",
                table: "Contracten",
                column: "ContractArchiefId",
                principalTable: "ContractenArchief",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracten_ContractenArchief_ContractArchiefId",
                table: "Contracten");

            migrationBuilder.DropTable(
                name: "ContractenArchief");

            migrationBuilder.DropIndex(
                name: "IX_Contracten_ContractArchiefId",
                table: "Contracten");

            migrationBuilder.DropColumn(
                name: "ContractArchiefId",
                table: "Contracten");

            migrationBuilder.CreateTable(
                name: "Oudecontracten",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeginDatum = table.Column<DateTime>(nullable: false),
                    Consultant = table.Column<string>(nullable: true),
                    EindDatum = table.Column<DateTime>(nullable: false),
                    Klant = table.Column<string>(nullable: true),
                    Kosten = table.Column<decimal>(nullable: true),
                    Subklant = table.Column<string>(nullable: true),
                    Tarief = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oudecontracten", x => x.Id);
                });
        }
    }
}
