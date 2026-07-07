using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MutualFundSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SchemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundHouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectFund = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nav = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return1Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return3M = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return6M = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Aum = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpenseRatio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Risk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskRating = table.Column<int>(type: "int", nullable: true),
                    AvailableForInvestment = table.Column<bool>(type: "bit", nullable: false),
                    SipAllowed = table.Column<bool>(type: "bit", nullable: false),
                    LumpsumAllowed = table.Column<bool>(type: "bit", nullable: false),
                    LaunchDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExitLoad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MutualFundSchemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MutualFundSchemes_SchemeId",
                table: "MutualFundSchemes",
                column: "SchemeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MutualFundSchemes");
        }
    }
}
