using Microsoft.EntityFrameworkCore;

namespace Stock.Data;

public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    public DbSet<EquityListingEntity> EquityListings => Set<EquityListingEntity>();

    public DbSet<IndicesEntity> Indices => Set<IndicesEntity>();

    // Separate table for full allIndices payload
    public DbSet<AllIndicesEntity> AllIndices => Set<AllIndicesEntity>();

    public DbSet<SymbolDataEntity> SymbolDataEntities => Set<SymbolDataEntity>();

    public DbSet<EquitySymbolDataEntity> EquitySymbolDataEntities => Set<EquitySymbolDataEntity>();

    public DbSet<YearwiseDataEntity> YearwiseDataEntities => Set<YearwiseDataEntity>();

    public DbSet<HistoricalTradeDataEntity> HistoricalTradeDataEntities => Set<HistoricalTradeDataEntity>();

    public DbSet<IndexDataEntity> IndexDataEntities => Set<IndexDataEntity>();

    public DbSet<ShareholdingPatternEntryEntity> ShareholdingPatternEntries => Set<ShareholdingPatternEntryEntity>();

    public DbSet<PeerComparisonDataEntity> PeerComparisonDataEntities => Set<PeerComparisonDataEntity>();

    // Entities for symbol-level splits
    public DbSet<OrderBookEntity> OrderBookEntities => Set<OrderBookEntity>();
    public DbSet<MetaDataEntity> MetaDataEntities => Set<MetaDataEntity>();
    public DbSet<TradeInfoEntity> TradeInfoEntities => Set<TradeInfoEntity>();
    public DbSet<PriceInfoEntity> PriceInfoEntities => Set<PriceInfoEntity>();
    public DbSet<SecInfoEntity> SecInfoEntities => Set<SecInfoEntity>();

    // Added DbSets for corporate-related data models
    public DbSet<CorpBoardMeetingEntity> CorpBoardMeetings => Set<CorpBoardMeetingEntity>();
    public DbSet<FinancialStatusEntity> FinancialStatuses => Set<FinancialStatusEntity>();
    public DbSet<CorpEventCalendarEntity> CorpEventCalendars => Set<CorpEventCalendarEntity>();
    public DbSet<CorpAnnualReportEntity> CorpAnnualReports => Set<CorpAnnualReportEntity>();
    public DbSet<CorpActionEntity> CorpActions => Set<CorpActionEntity>();
    public DbSet<CorporateAnnouncementEntity> CorporateAnnouncements => Set<CorporateAnnouncementEntity>();
    public DbSet<YearwiseStockSummaryEntity> YearwiseStockSummaries => Set<YearwiseStockSummaryEntity>();
    public DbSet<FavoriteSymbolEntity> FavoriteSymbolEntities => Set<FavoriteSymbolEntity>();

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

        modelBuilder.Entity<YearwiseStockSummaryEntity>()
            .HasNoKey()
            .ToView("vw_YearwiseStockSummary");
    }
}
