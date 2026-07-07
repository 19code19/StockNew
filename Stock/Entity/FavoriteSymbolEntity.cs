namespace Stock.Entity;

public class FavoriteSymbolEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string AssetType { get; set; } = "stock";
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
