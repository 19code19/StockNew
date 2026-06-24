namespace Stock.Model;

public class SymbolDataResponse
{
    [JsonPropertyName("equityResponse")]
    public List<EquitySymbolData> EquityResponse { get; set; } = new();
    public string Symbol { get; set; }
    public string Series { get; set; }
    public string MarketType { get; set; }

}

public class EquitySymbolData
{
    [JsonPropertyName("orderBook")]
    public OrderBook? OrderBook { get; set; }

    [JsonPropertyName("metaData")]
    public MetaData? MetaData { get; set; }

    [JsonPropertyName("tradeInfo")]
    public TradeInfo? TradeInfo { get; set; }

    [JsonPropertyName("priceInfo")]
    public PriceInfo? PriceInfo { get; set; }

    [JsonPropertyName("secInfo")]
    public SecInfo? SecInfo { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public string LastUpdateTime { get; set; } = string.Empty;
}

public class OrderBook
{
    [JsonPropertyName("buyPrice1")] public decimal BuyPrice1 { get; set; }
    [JsonPropertyName("buyQuantity1")] public long BuyQuantity1 { get; set; }
    [JsonPropertyName("buyPrice2")] public decimal BuyPrice2 { get; set; }
    [JsonPropertyName("buyQuantity2")] public long BuyQuantity2 { get; set; }
    [JsonPropertyName("buyPrice3")] public decimal BuyPrice3 { get; set; }
    [JsonPropertyName("buyQuantity3")] public long BuyQuantity3 { get; set; }
    [JsonPropertyName("buyPrice4")] public decimal BuyPrice4 { get; set; }
    [JsonPropertyName("buyQuantity4")] public long BuyQuantity4 { get; set; }
    [JsonPropertyName("buyPrice5")] public decimal BuyPrice5 { get; set; }
    [JsonPropertyName("buyQuantity5")] public long BuyQuantity5 { get; set; }

    [JsonPropertyName("sellPrice1")] public decimal SellPrice1 { get; set; }
    [JsonPropertyName("sellQuantity1")] public long SellQuantity1 { get; set; }
    [JsonPropertyName("sellPrice2")] public decimal SellPrice2 { get; set; }
    [JsonPropertyName("sellQuantity2")] public long SellQuantity2 { get; set; }
    [JsonPropertyName("sellPrice3")] public decimal SellPrice3 { get; set; }
    [JsonPropertyName("sellQuantity3")] public long SellQuantity3 { get; set; }
    [JsonPropertyName("sellPrice4")] public decimal SellPrice4 { get; set; }
    [JsonPropertyName("sellQuantity4")] public long SellQuantity4 { get; set; }
    [JsonPropertyName("sellPrice5")] public decimal SellPrice5 { get; set; }
    [JsonPropertyName("sellQuantity5")] public long SellQuantity5 { get; set; }

    [JsonPropertyName("lastPrice")] public decimal LastPrice { get; set; }
    [JsonPropertyName("totalBuyQuantity")] public long TotalBuyQuantity { get; set; }
    [JsonPropertyName("totalSellQuantity")] public long TotalSellQuantity { get; set; }
    [JsonPropertyName("perBuyQty")] public decimal PerBuyQty { get; set; }
    [JsonPropertyName("perSellQty")] public decimal PerSellQty { get; set; }
}

public class MetaData
{
    [JsonPropertyName("identifier")] public string Identifier { get; set; } = string.Empty;
    [JsonPropertyName("companyName")] public string CompanyName { get; set; } = string.Empty;
    [JsonPropertyName("isinCode")] public string IsinCode { get; set; } = string.Empty;
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = string.Empty;
    [JsonPropertyName("series")] public string Series { get; set; } = string.Empty;
    [JsonPropertyName("marketType")] public string MarketType { get; set; } = string.Empty;
    [JsonPropertyName("open")] public decimal Open { get; set; }
    [JsonPropertyName("dayHigh")] public decimal DayHigh { get; set; }
    [JsonPropertyName("dayLow")] public decimal DayLow { get; set; }
    [JsonPropertyName("previousClose")] public decimal PreviousClose { get; set; }
    [JsonPropertyName("averagePrice")] public decimal AveragePrice { get; set; }
    [JsonPropertyName("change")] public decimal Change { get; set; }
    [JsonPropertyName("basePrice")] public decimal BasePrice { get; set; }
    [JsonPropertyName("closePrice")] public decimal ClosePrice { get; set; }
    [JsonPropertyName("indicativeClose")] public decimal IndicativeClose { get; set; }
    [JsonPropertyName("ic_change")] public decimal IcChange { get; set; }
    [JsonPropertyName("ic_pchange")] public decimal IcPchange { get; set; }
    [JsonPropertyName("spoChange")] public decimal SpoChange { get; set; }
    [JsonPropertyName("spoPchange")] public decimal SpoPchange { get; set; }
    [JsonPropertyName("symbolStatus")] public string SymbolStatus { get; set; } = string.Empty;
    [JsonPropertyName("adjPrice")] public decimal AdjPrice { get; set; }
    [JsonPropertyName("iep")] public decimal Iep { get; set; }
    [JsonPropertyName("ieq")] public decimal Ieq { get; set; }
    [JsonPropertyName("pChange")] public decimal PChange { get; set; }
}

public class TradeInfo
{
    [JsonPropertyName("totalTradedVolume")] public long TotalTradedVolume { get; set; }
    [JsonPropertyName("totalTradedValue")] public decimal TotalTradedValue { get; set; }
    [JsonPropertyName("series")] public string Series { get; set; } = string.Empty;
    [JsonPropertyName("lastPrice")] public decimal LastPrice { get; set; }
    [JsonPropertyName("issuedSize")] public long IssuedSize { get; set; }
    [JsonPropertyName("basePrice")] public decimal BasePrice { get; set; }
    [JsonPropertyName("ffmc")] public decimal Ffmc { get; set; }
    [JsonPropertyName("faceValue")] public decimal FaceValue { get; set; }
    [JsonPropertyName("impactCost")] public decimal ImpactCost { get; set; }
    [JsonPropertyName("deliveryToTradedQuantity")] public decimal DeliveryToTradedQuantity { get; set; }
    [JsonPropertyName("applicableMargin")] public decimal ApplicableMargin { get; set; }
    [JsonPropertyName("marketLot")] public int? MarketLot { get; set; }
    [JsonPropertyName("quantitytraded")] public long QuantityTraded { get; set; }
    [JsonPropertyName("deliveryquantity")] public long DeliveryQuantity { get; set; }
    [JsonPropertyName("totalMarketCap")] public decimal TotalMarketCap { get; set; }
    [JsonPropertyName("secwisedelposdate")] public string SecWiseDelPosDate { get; set; } = string.Empty;
}

public class PriceInfo
{
    [JsonPropertyName("yearHightDt")] public string YearHighDt { get; set; } = string.Empty;
    [JsonPropertyName("yearLowDt")] public string YearLowDt { get; set; } = string.Empty;
    [JsonPropertyName("yearHigh")] public decimal YearHigh { get; set; }
    [JsonPropertyName("yearLow")] public decimal YearLow { get; set; }
    [JsonPropertyName("cmDailyVolatility")] public string CmDailyVolatility { get; set; } = string.Empty;
    [JsonPropertyName("cmAnnualVolatility")] public string CmAnnualVolatility { get; set; } = string.Empty;
    [JsonPropertyName("tickSize")] public decimal TickSize { get; set; }
    [JsonPropertyName("inav")] public decimal Inav { get; set; }
    [JsonPropertyName("isINav")] public string? IsINav { get; set; }
    [JsonPropertyName("priceBand")] public string PriceBand { get; set; } = string.Empty;
    [JsonPropertyName("ppriceBand")] public string PPriceBand { get; set; } = string.Empty;
}

public class SecInfo
{
    [JsonPropertyName("secStatus")] public string SecStatus { get; set; } = string.Empty;
    [JsonPropertyName("listingDate")] public string ListingDate { get; set; } = string.Empty;
    [JsonPropertyName("pdSectorInd")] public string PdSectorInd { get; set; } = string.Empty;
    [JsonPropertyName("pdSectorPe")] public decimal? PdSectorPe { get; set; }
    [JsonPropertyName("pdSymbolPe")] public decimal? PdSymbolPe { get; set; }
    [JsonPropertyName("isSuspended")] public string IsSuspended { get; set; } = string.Empty;
    [JsonPropertyName("basicIndustry")] public string BasicIndustry { get; set; } = string.Empty;
    [JsonPropertyName("index")] public string Index { get; set; } = string.Empty;
    [JsonPropertyName("deliveryQuantity")] public string DeliveryQuantity { get; set; } = string.Empty;
    [JsonPropertyName("deliveryTotradedQuantity")] public string DeliveryTotradedQuantity { get; set; } = string.Empty;
    [JsonPropertyName("securityvar")] public string SecurityVar { get; set; } = string.Empty;
    [JsonPropertyName("indexvar")] public string IndexVar { get; set; } = string.Empty;
    [JsonPropertyName("extremelossMargin")] public string ExtremeLossMargin { get; set; } = string.Empty;
    [JsonPropertyName("varMargin")] public string VarMargin { get; set; } = string.Empty;
    [JsonPropertyName("adhocMargin")] public string AdhocMargin { get; set; } = string.Empty;
    [JsonPropertyName("applicableMargin")] public string ApplicableMargin { get; set; } = string.Empty;
    [JsonPropertyName("bondType")] public string? BondType { get; set; }
    [JsonPropertyName("issueDesc")] public string? IssueDesc { get; set; }
    [JsonPropertyName("issueDate")] public string? IssueDate { get; set; }
    [JsonPropertyName("maturityDate")] public string? MaturityDate { get; set; }
    [JsonPropertyName("couponRate")] public string? CouponRate { get; set; }
    [JsonPropertyName("nxtIpDate")] public string? NxtIpDate { get; set; }
    [JsonPropertyName("creditRating")] public string? CreditRating { get; set; }
    [JsonPropertyName("macro")] public string Macro { get; set; } = string.Empty;
    [JsonPropertyName("sector")] public string Sector { get; set; } = string.Empty;
    [JsonPropertyName("industryInfo")] public string IndustryInfo { get; set; } = string.Empty;
    [JsonPropertyName("indexList")] public List<string> IndexList { get; set; } = new();
    [JsonPropertyName("boardStatus")] public string BoardStatus { get; set; } = string.Empty;
    [JsonPropertyName("tradingSegment")] public string TradingSegment { get; set; } = string.Empty;
    [JsonPropertyName("sessionNo")] public string? SessionNo { get; set; }
    [JsonPropertyName("classShare")] public string ClassShare { get; set; } = string.Empty;
    [JsonPropertyName("nameOfComplianceOfficer")] public string? NameOfComplianceOfficer { get; set; }
    [JsonPropertyName("sddcompliance")] public string? SddCompliance { get; set; }
}