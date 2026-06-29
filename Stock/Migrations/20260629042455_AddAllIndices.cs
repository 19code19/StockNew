using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class AddAllIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllIndices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last = table.Column<double>(type: "float", nullable: false),
                    Variation = table.Column<double>(type: "float", nullable: false),
                    PercentChange = table.Column<double>(type: "float", nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    PreviousClose = table.Column<double>(type: "float", nullable: false),
                    YearHigh = table.Column<double>(type: "float", nullable: false),
                    YearLow = table.Column<double>(type: "float", nullable: false),
                    PE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Advances = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Declines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unchanged = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerChange365d = table.Column<double>(type: "float", nullable: false),
                    PerChange30d = table.Column<double>(type: "float", nullable: false),
                    ChartTodayPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllIndices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllIndices");
        }
    }
}
