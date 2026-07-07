using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Amc",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DirectSchemeName",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DocRequired",
                table: "MutualFundSchemes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundManager",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GrowwVerdictScore",
                table: "MutualFundSchemes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinInvestmentAmount",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinSipInvestment",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageView",
                table: "MutualFundSchemes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Return10Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Return1D",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Return3Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Return5Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Return7Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchemeType",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SipReturn10Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SipReturn1Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SipReturn3Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SipReturn5Y",
                table: "MutualFundSchemes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubSubCategory",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "MutualFundSchemes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amc",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "DirectSchemeName",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "DocRequired",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "DocType",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "FundManager",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "GrowwVerdictScore",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "MinInvestmentAmount",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "MinSipInvestment",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "PageView",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "Return10Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "Return1D",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "Return3Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "Return5Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "Return7Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "SchemeType",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "SipReturn10Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "SipReturn1Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "SipReturn3Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "SipReturn5Y",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "SubSubCategory",
                table: "MutualFundSchemes");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "MutualFundSchemes");
        }
    }
}
