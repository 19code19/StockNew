namespace Stock.Helpers;

public static class EquityListCsvParser
{
    public static List<Stock.Model.EquityListing> Parse(string csvContent)
    {
        using var reader = new StringReader(csvContent);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.Trim().ToUpperInvariant(),
            TrimOptions = TrimOptions.Trim
        };

        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<Stock.Model.EquityListing>().ToList();
    }
}