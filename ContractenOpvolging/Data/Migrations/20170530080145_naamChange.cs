using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractenOpvolging.Data.Migrations
{
    public partial class naamChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefoon",
                table: "Consultants",
                newName: "Tel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tel",
                table: "Consultants",
                newName: "Telefoon");
        }
    }
}
