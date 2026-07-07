using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquityListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfListing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidUpValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarketLot = table.Column<int>(type: "int", nullable: false),
                    ISINNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquityListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteSymbolEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteSymbolEntities", x => x.Id);
                });

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
                    SchemeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowwVerdictScore = table.Column<int>(type: "int", nullable: true),
                    Nav = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return1D = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return1Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return3M = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return3Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return5Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return6M = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return7Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Return10Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SipReturn1Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SipReturn3Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SipReturn5Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SipReturn10Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DocRequired = table.Column<bool>(type: "bit", nullable: false),
                    MinInvestmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinSipInvestment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectSchemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageView = table.Column<int>(type: "int", nullable: true),
                    SubSubCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "SymbolDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolDataEntities", x => x.Id);
                    table.UniqueConstraint("AK_SymbolDataEntities_Symbol", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "YearwiseDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YesterdayChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneWeekChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneMonthChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThreeMonthChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SixMonthChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TwoYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThreeYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FiveYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneWeekDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexYesterdayChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexOneWeekChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexOneMonthChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexThreeMonthChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexSixMonthChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexOneYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexTwoYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexThreeYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexFiveYearChangePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexOneWeekDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearwiseDataEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndicativeClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IcChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IcPchange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpoChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpoPchange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SymbolStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdjPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Iep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ieq = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaDataEntities", x => x.Id);
                    table.UniqueConstraint("AK_MetaDataEntities_Symbol", x => x.Symbol);
                    table.ForeignKey(
                        name: "FK_MetaDataEntities_SymbolDataEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "SymbolDataEntities",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBookEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuyPrice1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyQuantity1 = table.Column<long>(type: "bigint", nullable: false),
                    BuyPrice2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyQuantity2 = table.Column<long>(type: "bigint", nullable: false),
                    BuyPrice3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyQuantity3 = table.Column<long>(type: "bigint", nullable: false),
                    BuyPrice4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyQuantity4 = table.Column<long>(type: "bigint", nullable: false),
                    BuyPrice5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyQuantity5 = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellQuantity1 = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellQuantity2 = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellQuantity3 = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellQuantity4 = table.Column<long>(type: "bigint", nullable: false),
                    SellPrice5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellQuantity5 = table.Column<long>(type: "bigint", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBuyQuantity = table.Column<long>(type: "bigint", nullable: false),
                    TotalSellQuantity = table.Column<long>(type: "bigint", nullable: false),
                    PerBuyQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerSellQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBookEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderBookEntities_SymbolDataEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "SymbolDataEntities",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceInfoEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearHighDt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearLowDt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CmDailyVolatility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CmAnnualVolatility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TickSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inav = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsINav = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceBand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PPriceBand = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceInfoEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceInfoEntities_SymbolDataEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "SymbolDataEntities",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecInfoEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PdSectorInd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PdSectorPe = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PdSymbolPe = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsSuspended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicIndustry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryQuantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryTotradedQuantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityVar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexVar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtremeLossMargin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarMargin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdhocMargin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicableMargin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BondType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaturityDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NxtIpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexListJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TradingSegment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassShare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfComplianceOfficer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SddCompliance = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecInfoEntities", x => x.Id);
                    table.UniqueConstraint("AK_SecInfoEntities_Symbol", x => x.Symbol);
                    table.ForeignKey(
                        name: "FK_SecInfoEntities_SymbolDataEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "SymbolDataEntities",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeInfoEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalTradedVolume = table.Column<long>(type: "bigint", nullable: false),
                    TotalTradedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IssuedSize = table.Column<long>(type: "bigint", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ffmc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FaceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpactCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryToTradedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicableMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarketLot = table.Column<int>(type: "int", nullable: true),
                    QuantityTraded = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryQuantity = table.Column<long>(type: "bigint", nullable: false),
                    TotalMarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SecWiseDelPosDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeInfoEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeInfoEntities_SymbolDataEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "SymbolDataEntities",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiRecommendations_MetaDataEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "MetaDataEntities",
                        principalColumn: "Symbol");
                    table.ForeignKey(
                        name: "FK_AiRecommendations_SecInfoEntities_Symbol",
                        column: x => x.Symbol,
                        principalTable: "SecInfoEntities",
                        principalColumn: "Symbol");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiRecommendations_Symbol",
                table: "AiRecommendations",
                column: "Symbol");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSymbolEntities_AssetType_Symbol",
                table: "FavoriteSymbolEntities",
                columns: new[] { "AssetType", "Symbol" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MetaDataEntities_Symbol",
                table: "MetaDataEntities",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MutualFundSchemes_SchemeId",
                table: "MutualFundSchemes",
                column: "SchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBookEntities_Symbol",
                table: "OrderBookEntities",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfoEntities_Symbol",
                table: "PriceInfoEntities",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecInfoEntities_Symbol",
                table: "SecInfoEntities",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymbolDataEntities_Symbol",
                table: "SymbolDataEntities",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradeInfoEntities_Symbol",
                table: "TradeInfoEntities",
                column: "Symbol",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiRecommendations");

            migrationBuilder.DropTable(
                name: "EquityListings");

            migrationBuilder.DropTable(
                name: "FavoriteSymbolEntities");

            migrationBuilder.DropTable(
                name: "MutualFundSchemes");

            migrationBuilder.DropTable(
                name: "OrderBookEntities");

            migrationBuilder.DropTable(
                name: "PriceInfoEntities");

            migrationBuilder.DropTable(
                name: "TradeInfoEntities");

            migrationBuilder.DropTable(
                name: "YearwiseDataEntities");

            migrationBuilder.DropTable(
                name: "MetaDataEntities");

            migrationBuilder.DropTable(
                name: "SecInfoEntities");

            migrationBuilder.DropTable(
                name: "SymbolDataEntities");
        }
    }
}
