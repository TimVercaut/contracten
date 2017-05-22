using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class naarOudeContractenModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oudecontracten");
        }
    }
}
