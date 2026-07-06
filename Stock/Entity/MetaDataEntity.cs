namespace Stock.Entity;

public class MetaDataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Identifier { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string IsinCode { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string MarketType { get; set; } = string.Empty;
    public decimal Open { get; set; }
    public decimal DayHigh { get; set; }
    public decimal DayLow { get; set; }
    public decimal PreviousClose { get; set; }
    public decimal AveragePrice { get; set; }
    public decimal Change { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal IndicativeClose { get; set; }
    public decimal IcChange { get; set; }
    public decimal IcPchange { get; set; }
    public decimal SpoChange { get; set; }
    public decimal SpoPchange { get; set; }
    public string SymbolStatus { get; set; } = string.Empty;
    public decimal AdjPrice { get; set; }
    public decimal Iep { get; set; }
    public decimal Ieq { get; set; }
    public decimal PChange { get; set; }

}
