using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KS.Core.Migrations
{
    public partial class InitialCreatece : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Kurumin");

            migrationBuilder.CreateTable(
                name: "Parameter",
                schema: "Kurumin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Base = table.Column<string>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameter",
                schema: "Kurumin");
        }
    }
}
