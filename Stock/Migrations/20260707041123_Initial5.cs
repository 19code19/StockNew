using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MutualFundSchemes_SchemeId",
                table: "MutualFundSchemes");

            migrationBuilder.CreateIndex(
                name: "IX_MutualFundSchemes_SchemeId",
                table: "MutualFundSchemes",
                column: "SchemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MutualFundSchemes_SchemeId",
                table: "MutualFundSchemes");

            migrationBuilder.CreateIndex(
                name: "IX_MutualFundSchemes_SchemeId",
                table: "MutualFundSchemes",
                column: "SchemeId",
                unique: true);
        }
    }
}
