using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosttrackerTask.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USD = table.Column<double>(type: "float", nullable: false),
                    EUR = table.Column<double>(type: "float", nullable: false),
                    AUD = table.Column<double>(type: "float", nullable: false),
                    CAD = table.Column<double>(type: "float", nullable: false),
                    PLN = table.Column<double>(type: "float", nullable: false),
                    MXN = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
