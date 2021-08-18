namespace HexiTech.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        public DbSet<UserShoppingCart> UserShoppingCarts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProductType>()
                .HasOne(pt => pt.Category)
                .WithMany(c => c.ProductTypes)
                .HasForeignKey(pt => pt.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductId);

            builder
                .Entity<UserShoppingCart>()
                .HasKey(usc => new { usc.UserId, usc.ProductId });

            builder
                .Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            base.OnModelCreating(builder);
        }
    }
}