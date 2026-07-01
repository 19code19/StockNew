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
                name: "CorpActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceVal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BcStartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BcEndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NdStartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NdEndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaBroadcastDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorpAnnualReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromYr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToYr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BroadcastDttm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisseminationDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeTaken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttFileSize = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpAnnualReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorpBoardMeetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmPurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmIndustry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmTimestamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmIsin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ixbrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diff = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SysTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttFileSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IxbrlFileSize = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpBoardMeetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorpEventCalendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmPurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmIndustry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmTimestamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmIsin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmDt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmTimestampFull = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmAnSeqId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BmAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpEventCalendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorporateAnnouncements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttchmntFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmIsin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnDt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeqId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmIndustry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orgid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttchmntText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bflag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CsvName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchDissTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttFileSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasXbrl = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateAnnouncements", x => x.Id);
                });

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
                name: "FinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDateMonYr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expenditure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Audited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cumulative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consolidated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eps = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReProLossBefTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetProLossAftTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReBroadcastTimestamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalTradeDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ch52WeekHighPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ch52WeekLowPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChLastTradedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChOpeningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChPreviousClsPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChSeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChTotTradedQty = table.Column<long>(type: "bigint", nullable: false),
                    ChTotTradedVal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChTotalTrades = table.Column<long>(type: "bigint", nullable: false),
                    ChTradeHighPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChTradeLowPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MTimestamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vwap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalTradeDataEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndexDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Last = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeVal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstituentsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicativeClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IcChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IcPerChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsConstituents = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexDataEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indices",
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
                    table.PrimaryKey("PK_Indices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "OrderBookEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "PeerComparisonDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quarter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false),
                    Eps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ltp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebtEqRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PromoterHolding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PChangeUpper = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerComparisonDataEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceInfoEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "SecInfoEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "ShareholdingPatternEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ndsid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareholdingPatternEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SymbolDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolDataEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeInfoEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "EquitySymbolDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MetaDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TradeInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SecInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastUpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquitySymbolDataEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquitySymbolDataEntities_MetaDataEntities_MetaDataId",
                        column: x => x.MetaDataId,
                        principalTable: "MetaDataEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquitySymbolDataEntities_OrderBookEntities_OrderBookId",
                        column: x => x.OrderBookId,
                        principalTable: "OrderBookEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquitySymbolDataEntities_PriceInfoEntities_PriceInfoId",
                        column: x => x.PriceInfoId,
                        principalTable: "PriceInfoEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquitySymbolDataEntities_SecInfoEntities_SecInfoId",
                        column: x => x.SecInfoId,
                        principalTable: "SecInfoEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquitySymbolDataEntities_SymbolDataEntities_SymbolDataId",
                        column: x => x.SymbolDataId,
                        principalTable: "SymbolDataEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquitySymbolDataEntities_TradeInfoEntities_TradeInfoId",
                        column: x => x.TradeInfoId,
                        principalTable: "TradeInfoEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquitySymbolDataEntities_MetaDataId",
                table: "EquitySymbolDataEntities",
                column: "MetaDataId");

            migrationBuilder.CreateIndex(
                name: "IX_EquitySymbolDataEntities_OrderBookId",
                table: "EquitySymbolDataEntities",
                column: "OrderBookId");

            migrationBuilder.CreateIndex(
                name: "IX_EquitySymbolDataEntities_PriceInfoId",
                table: "EquitySymbolDataEntities",
                column: "PriceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquitySymbolDataEntities_SecInfoId",
                table: "EquitySymbolDataEntities",
                column: "SecInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquitySymbolDataEntities_SymbolDataId",
                table: "EquitySymbolDataEntities",
                column: "SymbolDataId");

            migrationBuilder.CreateIndex(
                name: "IX_EquitySymbolDataEntities_TradeInfoId",
                table: "EquitySymbolDataEntities",
                column: "TradeInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorpActions");

            migrationBuilder.DropTable(
                name: "CorpAnnualReports");

            migrationBuilder.DropTable(
                name: "CorpBoardMeetings");

            migrationBuilder.DropTable(
                name: "CorpEventCalendars");

            migrationBuilder.DropTable(
                name: "CorporateAnnouncements");

            migrationBuilder.DropTable(
                name: "EquityListings");

            migrationBuilder.DropTable(
                name: "EquitySymbolDataEntities");

            migrationBuilder.DropTable(
                name: "FinancialStatuses");

            migrationBuilder.DropTable(
                name: "HistoricalTradeDataEntities");

            migrationBuilder.DropTable(
                name: "IndexDataEntities");

            migrationBuilder.DropTable(
                name: "Indices");

            migrationBuilder.DropTable(
                name: "PeerComparisonDataEntities");

            migrationBuilder.DropTable(
                name: "ShareholdingPatternEntries");

            migrationBuilder.DropTable(
                name: "YearwiseDataEntities");

            migrationBuilder.DropTable(
                name: "MetaDataEntities");

            migrationBuilder.DropTable(
                name: "OrderBookEntities");

            migrationBuilder.DropTable(
                name: "PriceInfoEntities");

            migrationBuilder.DropTable(
                name: "SecInfoEntities");

            migrationBuilder.DropTable(
                name: "SymbolDataEntities");

            migrationBuilder.DropTable(
                name: "TradeInfoEntities");
        }
    }
}
