using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class nogeenkeer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracten_Klanten_Klant",
                table: "Contracten");

            migrationBuilder.RenameColumn(
                name: "Startdatum",
                table: "Contracten",
                newName: "StartDatum");

            migrationBuilder.RenameColumn(
                name: "Subklant",
                table: "Contracten",
                newName: "OnderKlant");

            migrationBuilder.RenameColumn(
                name: "Klant",
                table: "Contracten",
                newName: "KlantID");

            migrationBuilder.RenameColumn(
                name: "Einddatum",
                table: "Contracten",
                newName: "EindDatum");

            migrationBuilder.RenameIndex(
                name: "IX_Contracten_Klant",
                table: "Contracten",
                newName: "IX_Contracten_KlantID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDatum",
                table: "Contracten",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EindDatum",
                table: "Contracten",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracten_Klanten_KlantID",
                table: "Contracten",
                column: "KlantID",
                principalTable: "Klanten",
                principalColumn: "KlantID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracten_Klanten_KlantID",
                table: "Contracten");

            migrationBuilder.RenameColumn(
                name: "StartDatum",
                table: "Contracten",
                newName: "Startdatum");

            migrationBuilder.RenameColumn(
                name: "OnderKlant",
                table: "Contracten",
                newName: "Subklant");

            migrationBuilder.RenameColumn(
                name: "KlantID",
                table: "Contracten",
                newName: "Klant");

            migrationBuilder.RenameColumn(
                name: "EindDatum",
                table: "Contracten",
                newName: "Einddatum");

            migrationBuilder.RenameIndex(
                name: "IX_Contracten_KlantID",
                table: "Contracten",
                newName: "IX_Contracten_Klant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Startdatum",
                table: "Contracten",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Einddatum",
                table: "Contracten",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_Contracten_Klanten_Klant",
                table: "Contracten",
                column: "Klant",
                principalTable: "Klanten",
                principalColumn: "KlantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
