namespace Stock.Model;

public class EquityListing
{
    [Name("SYMBOL")]
    public string Symbol { get; set; } = string.Empty;

    [Name("NAME OF COMPANY")]
    public string NameOfCompany { get; set; } = string.Empty;

    [Name(" SERIES")]
    public string Series { get; set; } = string.Empty;

    [Name(" DATE OF LISTING")]
    [TypeConverter(typeof(EquityListingDateConverter))]
    public DateTime? DateOfListing { get; set; }

    [Name(" PAID UP VALUE")]
    public decimal PaidUpValue { get; set; }

    [Name(" MARKET LOT")]
    public int MarketLot { get; set; }

    [Name(" ISIN NUMBER")]
    public string ISINNumber { get; set; } = string.Empty;

    [Name(" FACE VALUE")]
    public decimal FaceValue { get; set; }
}