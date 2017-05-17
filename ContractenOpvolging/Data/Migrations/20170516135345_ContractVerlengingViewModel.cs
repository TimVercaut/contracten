using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class ContractVerlengingViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractVerlengingViewModel",
                columns: table => new
                {
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Consultant = table.Column<string>(nullable: true),
                    EindDatum = table.Column<DateTime>(nullable: false),
                    Klant = table.Column<string>(nullable: true),
                    NieuweEindDatum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractVerlengingViewModel", x => x.ContractID);
                });

            migrationBuilder.CreateTable(
                name: "ResetRolesViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetRolesViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractVerlengingViewModel");

            migrationBuilder.DropTable(
                name: "ResetRolesViewModel");
        }
    }
}
