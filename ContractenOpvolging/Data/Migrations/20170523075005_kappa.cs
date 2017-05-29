using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class kappa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracten_ContractenArchief_ContractArchiefId",
                table: "Contracten");

            migrationBuilder.DropIndex(
                name: "IX_Contracten_ContractArchiefId",
                table: "Contracten");

            migrationBuilder.DropColumn(
                name: "ContractArchiefId",
                table: "Contracten");

            migrationBuilder.RenameColumn(
                name: "Jaar",
                table: "ContractenArchief",
                newName: "jaar");

            migrationBuilder.RenameColumn(
                name: "Naam",
                table: "ContractenArchief",
                newName: "Subklant");

            migrationBuilder.AddColumn<string>(
                name: "Consultant",
                table: "ContractenArchief",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EindDatum",
                table: "ContractenArchief",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Klant",
                table: "ContractenArchief",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Kost",
                table: "ContractenArchief",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDatum",
                table: "ContractenArchief",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Tarief",
                table: "ContractenArchief",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consultant",
                table: "ContractenArchief");

            migrationBuilder.DropColumn(
                name: "EindDatum",
                table: "ContractenArchief");

            migrationBuilder.DropColumn(
                name: "Klant",
                table: "ContractenArchief");

            migrationBuilder.DropColumn(
                name: "Kost",
                table: "ContractenArchief");

            migrationBuilder.DropColumn(
                name: "StartDatum",
                table: "ContractenArchief");

            migrationBuilder.DropColumn(
                name: "Tarief",
                table: "ContractenArchief");

            migrationBuilder.RenameColumn(
                name: "jaar",
                table: "ContractenArchief",
                newName: "Jaar");

            migrationBuilder.RenameColumn(
                name: "Subklant",
                table: "ContractenArchief",
                newName: "Naam");

            migrationBuilder.AddColumn<int>(
                name: "ContractArchiefId",
                table: "Contracten",
                nullable: true);

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
    }
}
