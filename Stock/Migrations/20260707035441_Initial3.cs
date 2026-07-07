using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "FavoriteSymbolEntities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AssetType",
                table: "FavoriteSymbolEntities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssetType",
                table: "AiRecommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSymbolEntities_AssetType_Symbol",
                table: "FavoriteSymbolEntities",
                columns: new[] { "AssetType", "Symbol" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoriteSymbolEntities_AssetType_Symbol",
                table: "FavoriteSymbolEntities");

            migrationBuilder.DropColumn(
                name: "AssetType",
                table: "FavoriteSymbolEntities");

            migrationBuilder.DropColumn(
                name: "AssetType",
                table: "AiRecommendations");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "FavoriteSymbolEntities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
