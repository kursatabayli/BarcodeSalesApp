using BarcodeSalesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<SalesRecordEntity> SalesRecords { get; set; }
    public DbSet<StockEntity> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SalesRecordEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<ProductEntity>()
        .HasOne(p => p.Stock)
        .WithOne(s => s.Product)
        .HasForeignKey<StockEntity>(s => s.ProductId);

        modelBuilder.Entity<StockEntity>()
                .HasKey(s => s.ProductId);

        modelBuilder.Entity<SalesRecordEntity>(builder =>
        {
            builder.HasOne(salesRecord => salesRecord.Product)
                .WithMany(product => product.SalesRecords)
                .HasForeignKey(salesRecord => salesRecord.ProductId);
            builder.HasIndex(salesRecord => salesRecord.SaleDate);
        });
    }
}