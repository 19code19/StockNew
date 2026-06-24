namespace Stock.Helpers;

public class EquityListingDateConverter : DateTimeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;

        return DateTime.TryParseExact(text.Trim(), "dd-MMM-yy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
            ? date
            : null;
    }
}