﻿namespace HexiTech.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HexiTechDbContext : IdentityDbContext<User>
    {
        public HexiTechDbContext(DbContextOptions<HexiTechDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<ProductType> ProductTypes { get; init; }

        public DbSet<ProductReview> ProductReviews { get; init; }

        public DbSet<CartItem> CartItems { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(t => t.ProductType)
                .WithMany(p => p.Products)
                .HasForeignKey(t => t.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProductType>()
                .HasOne(c => c.Category)
                .WithMany(t => t.ProductTypes)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProductReview>()
                .HasOne(p => p.Product)
                .WithMany(pr => pr.ProductReviews)
                .HasForeignKey(p => p.ProductId);

            base.OnModelCreating(builder);
        }
    }
}