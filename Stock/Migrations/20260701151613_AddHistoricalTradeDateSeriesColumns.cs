using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoricalTradeDateSeriesColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromDate",
                table: "HistoricalTradeDataEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "HistoricalTradeDataEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "ToDate",
                table: "HistoricalTradeDataEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "HistoricalTradeDataEntities");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "HistoricalTradeDataEntities");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "HistoricalTradeDataEntities");
        }
    }
}
