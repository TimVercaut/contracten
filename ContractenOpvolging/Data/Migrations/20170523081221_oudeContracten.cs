using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class oudeContracten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractenArchief",
                table: "ContractenArchief");

            migrationBuilder.RenameTable(
                name: "ContractenArchief",
                newName: "OudeContracten");

            migrationBuilder.RenameColumn(
                name: "jaar",
                table: "OudeContracten",
                newName: "Jaar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OudeContracten",
                table: "OudeContracten",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OudeContracten",
                table: "OudeContracten");

            migrationBuilder.RenameTable(
                name: "OudeContracten",
                newName: "ContractenArchief");

            migrationBuilder.RenameColumn(
                name: "Jaar",
                table: "ContractenArchief",
                newName: "jaar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractenArchief",
                table: "ContractenArchief",
                column: "Id");
        }
    }
}
