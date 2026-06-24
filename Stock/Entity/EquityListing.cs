namespace Stock.Entity;

public class EquityListing
{
    public string Symbol { get; set; } = string.Empty;

    public string NameOfCompany { get; set; } = string.Empty;

    public string Series { get; set; } = string.Empty;

    public DateTime? DateOfListing { get; set; }

    public decimal PaidUpValue { get; set; }

    public int MarketLot { get; set; }

    public string ISINNumber { get; set; } = string.Empty;

    public decimal FaceValue { get; set; }
}
