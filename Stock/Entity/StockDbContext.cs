using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Stock.Entity;

public class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    public DbSet<EquityListingEntity> EquityListings => Set<EquityListingEntity>();

    public DbSet<SymbolDataEntity> SymbolDataEntities => Set<SymbolDataEntity>();

    public DbSet<YearwiseDataEntity> YearwiseDataEntities => Set<YearwiseDataEntity>();

    public DbSet<MutualFundSchemeEntity> MutualFundSchemes => Set<MutualFundSchemeEntity>();

    // Entities for symbol-level splits
    public DbSet<OrderBookEntity> OrderBookEntities => Set<OrderBookEntity>();
    public DbSet<MetaDataEntity> MetaDataEntities => Set<MetaDataEntity>();
    public DbSet<TradeInfoEntity> TradeInfoEntities => Set<TradeInfoEntity>();
    public DbSet<PriceInfoEntity> PriceInfoEntities => Set<PriceInfoEntity>();
    public DbSet<SecInfoEntity> SecInfoEntities => Set<SecInfoEntity>();
    public DbSet<AiRecommendationEntity> AiRecommendations => Set<AiRecommendationEntity>();
    public DbSet<FavoriteSymbolEntity> FavoriteSymbolEntities => Set<FavoriteSymbolEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasAlternateKey(x => x.Symbol);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasOne(x => x.OrderBook)
            .WithOne()
            .HasForeignKey<OrderBookEntity>(x => x.Symbol)
            .HasPrincipalKey<SymbolDataEntity>(x => x.Symbol)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasOne(x => x.MetaData)
            .WithOne()
            .HasForeignKey<MetaDataEntity>(x => x.Symbol)
            .HasPrincipalKey<SymbolDataEntity>(x => x.Symbol)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasOne(x => x.TradeInfo)
            .WithOne()
            .HasForeignKey<TradeInfoEntity>(x => x.Symbol)
            .HasPrincipalKey<SymbolDataEntity>(x => x.Symbol)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasOne(x => x.PriceInfo)
            .WithOne()
            .HasForeignKey<PriceInfoEntity>(x => x.Symbol)
            .HasPrincipalKey<SymbolDataEntity>(x => x.Symbol)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SymbolDataEntity>()
            .HasOne(x => x.SecInfo)
            .WithOne()
            .HasForeignKey<SecInfoEntity>(x => x.Symbol)
            .HasPrincipalKey<SymbolDataEntity>(x => x.Symbol)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderBookEntity>()
            .HasIndex(x => x.Symbol)
            .IsUnique();

        modelBuilder.Entity<TradeInfoEntity>()
            .HasIndex(x => x.Symbol)
            .IsUnique();

        modelBuilder.Entity<PriceInfoEntity>()
            .HasIndex(x => x.Symbol)
            .IsUnique();

        modelBuilder.Entity<SymbolDataEntity>()
            .HasIndex(x => x.Symbol)
            .IsUnique();

        modelBuilder.Entity<MutualFundSchemeEntity>()
            .HasIndex(x => x.SchemeId);

        modelBuilder.Entity<FavoriteSymbolEntity>()
            .HasIndex(x => new { x.AssetType, x.Symbol })
            .IsUnique();

        var indexListConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

        var indexListComparer = new ValueComparer<List<string>>(
            (l1, l2) => l1.SequenceEqual(l2),
            l => l.Aggregate(0, (h, v) => HashCode.Combine(h, v == null ? 0 : v.GetHashCode())),
            l => l.ToList());

        modelBuilder.Entity<SecInfoEntity>()
            .Property(e => e.IndexList)
            .HasColumnName("IndexListJson")
            .HasConversion(indexListConverter)
            .Metadata.SetValueComparer(indexListComparer);

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
