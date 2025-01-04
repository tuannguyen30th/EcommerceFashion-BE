    using Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Infrastructure.Context
    {
        public class EcommerceFashionDbContext : DbContext
    {
            public EcommerceFashionDbContext(DbContextOptions<EcommerceFashionDbContext> options) : base(options)
            {
            }
            public DbSet<Account> Accounts { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<AccountRole> AccountRoles { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<Delivery> Deliveries { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartItem> CartItems { get; set; }
            public DbSet<OrderDetail> OrderDetails { get; set; }
            public DbSet<OrderDetailAttribute> OrderDetailAttributes { get; set; }
            public DbSet<Feedback> Feedbacks { get; set; }
            public DbSet<Image> Images { get; set; }
            public DbSet<ProductVideo> ProductVideos { get; set; }
            public DbSet<ProductAttribute> Attributes { get; set; }
            public DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
            public DbSet<ProductVariant> ProductVariants { get; set; }
            public DbSet<ProductVariantAttributeValueData> ProductVariantAttributeValueDatas { get; set; }
            public DbSet<AttributeSelection> AttributeSelections { get; set; }
            public DbSet<ProductCategory> ProductCategories { get; set; }
            public DbSet<ProductCategoryAttributeData> ProductCategoryAttributeDatas { get; set; }
            public DbSet<ProductCategoryData> ProductCategoryDatas { get; set; }
            public DbSet<BrandProductCategoryData> BrandProductCategoryDatas { get; set; }
            public DbSet<WishList> WishLists { get; set; }
            public DbSet<WishListAttribute> WishListAttributes { get; set; }
            public DbSet<Credit> Credits { get; set; }
            public DbSet<CreditHistory> CreditHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                   base.OnModelCreating(modelBuilder);
                   modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceFashionDbContext).Assembly);
                   modelBuilder.Entity<Account>(entity =>
                   {
                       entity.Property(account => account.FirstName).HasMaxLength(50);
                       entity.Property(account => account.LastName).HasMaxLength(50);
                       entity.Property(account => account.Username).HasMaxLength(50);
                       entity.Property(account => account.Email).HasMaxLength(256);
                       entity.Property(account => account.PhoneNumber).HasMaxLength(15);
                       entity.HasIndex(account => account.Username).IsUnique();
                       entity.HasIndex(account => account.Email).IsUnique();
                   });

            // Role entity configuration
                   modelBuilder.Entity<Role>(entity =>
                   {
                       entity.Property(role => role.Name).HasMaxLength(50);
                       entity.Property(role => role.Description).HasMaxLength(256);
                       entity.HasIndex(role => role.Name).IsUnique();
                   });
                   modelBuilder.Entity<WishList>()
                  .HasMany(w => w.WishListAttributes)
                  .WithOne()
                  .HasForeignKey(attr => attr.WishListId)
                  .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WishListAttribute>(entity =>
            {
                entity.HasOne(wl => wl.ProductVariantAttributeValueData)
                      .WithMany()
                      .HasForeignKey(wl => wl.ProductVariantAttributeValueDataId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            base.OnModelCreating(modelBuilder);

            // Feedback relationships
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Account)
                .WithMany(a => a.FeedBacksAsReviewer)
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Shop)
                .WithMany(a => a.FeedBacksAsShop)
                .HasForeignKey(f => f.ShopId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderDetail>()
               .HasOne(c => c.Product)
               .WithMany(p => p.OrderDetails)
               .HasForeignKey(c => c.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WishList>()
              .HasOne(c => c.Product)
              .WithMany(p => p.WishLists)
              .HasForeignKey(c => c.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CartItem>()
             .HasOne(c => c.ProductVariantAttributeValueData)
             .WithMany(p => p.CartItems)
             .HasForeignKey(c => c.ProductVariantAttributeValueDataId)
             .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderDetailAttribute>()
            .HasOne(c => c.ProductVariantAttributeValueData)
            .WithMany(p => p.OrderDetailAttributes)
            .HasForeignKey(c => c.ProductVariantAttributeValueDataId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WishListAttribute>()
          .HasOne(c => c.ProductVariantAttributeValueData)
          .WithMany(p => p.WishListAttributes)
          .HasForeignKey(c => c.ProductVariantAttributeValueDataId)
          .OnDelete(DeleteBehavior.Restrict);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-R23TMQS\\ALEXNGUYEN;uid=sa;pwd=12345;Database=EcommerceFashion;Trusted_Connection=True;TrustServerCertificate=True");
        //}
    }
    }
