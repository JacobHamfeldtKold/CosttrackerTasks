using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosttrackerTask.Migrations
{
    /// <inheritdoc />
    public partial class nameChangeIntoRates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "HistoricRate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricRate",
                table: "HistoricRate",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricRate",
                table: "HistoricRate");

            migrationBuilder.RenameTable(
                name: "HistoricRate",
                newName: "Customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");
        }
    }
}
