using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class RolesDrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NieuweKleur",
                table: "ContractVerlengingViewModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerlengKleur",
                table: "ContractVerlengingViewModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NieuweKleur",
                table: "ContractVerlengingViewModel");

            migrationBuilder.DropColumn(
                name: "VerlengKleur",
                table: "ContractVerlengingViewModel");
        }
    }
}
