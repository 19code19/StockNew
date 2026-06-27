using Microsoft.EntityFrameworkCore;

namespace Stock.Data;

public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    public DbSet<EquityListingEntity> EquityListings => Set<EquityListingEntity>();

    public DbSet<IndicesEntity> Indices => Set<IndicesEntity>();

    public DbSet<SymbolDataEntity> SymbolDataEntities => Set<SymbolDataEntity>();

    public DbSet<EquitySymbolDataEntity> EquitySymbolDataEntities => Set<EquitySymbolDataEntity>();

    public DbSet<YearwiseDataEntity> YearwiseDataEntities => Set<YearwiseDataEntity>();

    public DbSet<HistoricalTradeDataEntity> HistoricalTradeDataEntities => Set<HistoricalTradeDataEntity>();

    public DbSet<IndexDataEntity> IndexDataEntities => Set<IndexDataEntity>();

    public DbSet<ShareholdingPatternEntryEntity> ShareholdingPatternEntries => Set<ShareholdingPatternEntryEntity>();

    public DbSet<PeerComparisonDataEntity> PeerComparisonDataEntities => Set<PeerComparisonDataEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasMany(x => x.EquityResponse)
            .WithOne(x => x.SymbolData)
            .HasForeignKey(x => x.SymbolDataId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EquitySymbolDataEntity>()
            .HasOne(x => x.OrderBook)
            .WithMany()
            .HasForeignKey(x => x.OrderBookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EquitySymbolDataEntity>()
            .HasOne(x => x.MetaData)
            .WithMany()
            .HasForeignKey(x => x.MetaDataId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EquitySymbolDataEntity>()
            .HasOne(x => x.TradeInfo)
            .WithMany()
            .HasForeignKey(x => x.TradeInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EquitySymbolDataEntity>()
            .HasOne(x => x.PriceInfo)
            .WithMany()
            .HasForeignKey(x => x.PriceInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EquitySymbolDataEntity>()
            .HasOne(x => x.SecInfo)
            .WithMany()
            .HasForeignKey(x => x.SecInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
