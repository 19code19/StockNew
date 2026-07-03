using Microsoft.EntityFrameworkCore;

namespace Stock.Data;

[Keyless]
public class YearwiseStockSummaryEntity
{
    //YearwiseDataEntity
    public decimal YesterdayChangePercent { get; set; }
    public decimal OneWeekChangePercent { get; set; }
    public decimal OneMonthChangePercent { get; set; }
    public decimal ThreeMonthChangePercent { get; set; }
    public decimal SixMonthChangePercent { get; set; }
    public decimal OneYearChangePercent { get; set; }
    public decimal TwoYearChangePercent { get; set; }
    public decimal ThreeYearChangePercent { get; set; }
    public decimal FiveYearChangePercent { get; set; }
    public decimal IndexYesterdayChangePercent { get; set; }
    public decimal IndexOneWeekChangePercent { get; set; }
    public decimal IndexOneMonthChangePercent { get; set; }
    public decimal IndexThreeMonthChangePercent { get; set; }
    public decimal IndexSixMonthChangePercent { get; set; }
    public decimal IndexOneYearChangePercent { get; set; }
    public decimal IndexTwoYearChangePercent { get; set; }
    public decimal IndexThreeYearChangePercent { get; set; }
    public decimal IndexFiveYearChangePercent { get; set; }
    public string IndexName { get; set; } = string.Empty;

    //SecInfoEntity

    public string BasicIndustry { get; set; } = string.Empty;
    public string IsSuspended { get; set; } = string.Empty;
    public string Macro { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string IndustryInfo { get; set; } = string.Empty;

    //MetaDataEntities

    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
}
