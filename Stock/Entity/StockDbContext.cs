using Microsoft.EntityFrameworkCore;

namespace Stock.Entity;

public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    public DbSet<EquityListingEntity> EquityListings => Set<EquityListingEntity>();

    public DbSet<SymbolDataEntity> SymbolDataEntities => Set<SymbolDataEntity>();

    public DbSet<YearwiseDataEntity> YearwiseDataEntities => Set<YearwiseDataEntity>();

    // Entities for symbol-level splits
    public DbSet<OrderBookEntity> OrderBookEntities => Set<OrderBookEntity>();
    public DbSet<MetaDataEntity> MetaDataEntities => Set<MetaDataEntity>();
    public DbSet<TradeInfoEntity> TradeInfoEntities => Set<TradeInfoEntity>();
    public DbSet<PriceInfoEntity> PriceInfoEntities => Set<PriceInfoEntity>();
    public DbSet<SecInfoEntity> SecInfoEntities => Set<SecInfoEntity>();
    public DbSet<YearwiseStockSummaryEntity> YearwiseStockSummaries => Set<YearwiseStockSummaryEntity>();
    public DbSet<AiRecommendationEntity> AiRecommendations => Set<AiRecommendationEntity>();
    public DbSet<AiRecommendationViewEntity> AiRecommendationViews => Set<AiRecommendationViewEntity>();
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

        modelBuilder.Entity<MetaDataEntity>()
            .HasIndex(x => x.Symbol)
            .IsUnique();

        modelBuilder.Entity<SecInfoEntity>()
            .HasIndex(x => x.Symbol)
            .IsUnique();

        modelBuilder.Entity<AiRecommendationEntity>()
            .HasOne(x => x.MetaData)
            .WithMany()
            .HasForeignKey(x => x.Symbol)
            .HasPrincipalKey(x => x.Symbol)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AiRecommendationEntity>()
            .HasOne(x => x.SecInfo)
            .WithMany()
            .HasForeignKey(x => x.Symbol)
            .HasPrincipalKey(x => x.Symbol)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
