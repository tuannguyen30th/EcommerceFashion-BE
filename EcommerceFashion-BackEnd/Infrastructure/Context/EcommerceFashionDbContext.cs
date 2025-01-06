    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

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
            public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
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

            // Feedback 
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Shop)
                .WithMany()
                .HasForeignKey(f => f.ShopId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cart
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Account)
                .WithMany(a => a.Carts)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails) 
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails) 
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ProductVariantAttributeValueData)
                .WithMany()
                .HasForeignKey(ci => ci.ProductVariantAttributeValueDataId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderDetailAttribute
            modelBuilder.Entity<OrderDetailAttribute>()
                .HasOne(oda => oda.OrderDetail)
                .WithMany(od => od.OrderDetailAttributes) 
                .HasForeignKey(oda => oda.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetailAttribute>()
                .HasOne(oda => oda.ProductVariantAttributeValueData)
                .WithMany(pv => pv.OrderDetailAttributes) 
                .HasForeignKey(oda => oda.ProductVariantAttributeValueDataId)
                .OnDelete(DeleteBehavior.Restrict);


            // ProductVariantAttributeValueData
            modelBuilder.Entity<ProductVariantAttributeValueData>()
                .HasOne(p => p.ProductVariant) 
                .WithMany()  
                .HasForeignKey(p => p.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<ProductVariantAttributeValueData>()
                .HasOne(p => p.ProductAttributeValue)  
                .WithMany()  
                .HasForeignKey(p => p.ProductAttributeValueId)
                .OnDelete(DeleteBehavior.Restrict);

            // WishListAttribute
            modelBuilder.Entity<WishListAttribute>()
                .HasOne(wla => wla.WishList)
                .WithMany(wl => wl.WishListAttributes)
                .HasForeignKey(wla => wla.WishListId)
                .OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<WishListAttribute>()
                .HasOne(wla => wla.ProductVariantAttributeValueData)
                .WithMany() 
                .HasForeignKey(wla => wla.ProductVariantAttributeValueDataId)
                .OnDelete(DeleteBehavior.Cascade);
            // WishList
            modelBuilder.Entity<WishList>()
                .HasOne(c => c.Product)
                .WithMany(p => p.WishLists)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            // BrandProductCategoryData
            modelBuilder.Entity<BrandProductCategoryData>()
                .HasOne(bpcd => bpcd.Brand)
                .WithMany(b => b.BrandProductCategoryDatas)
                .HasForeignKey(bpcd => bpcd.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrandProductCategoryData>()
                .HasOne(bpcd => bpcd.ProductCategory)
                .WithMany(pc => pc.BrandProductCategoryDatas)
                .HasForeignKey(bpcd => bpcd.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            // CreditHistory
            modelBuilder.Entity<CreditHistory>()
                .HasOne(ch => ch.Credit)
                .WithMany()
                .HasForeignKey(ch => ch.CreditId)
                .OnDelete(DeleteBehavior.Cascade);
            // Image 
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images) 
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Feedback)
                .WithMany(f => f.Images) 
                .HasForeignKey(i => i.FeedbackId)
                .OnDelete(DeleteBehavior.SetNull);
            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithMany(d => d.Orders) 
                .HasForeignKey(o => o.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Account)
                .WithMany(a => a.Orders) 
                .HasForeignKey(o => o.CreatedById) 
                .OnDelete(DeleteBehavior.Restrict);
            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments) 
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product
            modelBuilder.Entity<Product>()
                 .HasOne(p => p.Brand)
                 .WithMany()
                 .HasForeignKey(p => p.BrandId)
                 .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Shop)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);
            // ProductAttributeValue
            modelBuilder.Entity<ProductAttributeValue>()
                .HasOne(pav => pav.Attribute)
                .WithMany()  
                .HasForeignKey(pav => pav.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);
            // ProductCategory
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Parent)  
                .WithMany(pc => pc.ProductCategories) 
                .HasForeignKey(pc => pc.ParentId) 
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.ProductCategoryDatas)
                .WithOne()
                .HasForeignKey(pc => pc.ProductCategoryId);
            // ProductCategoryAttributeData
            modelBuilder.Entity<ProductCategoryAttributeData>()
                .HasOne(pcad => pcad.ProductCategory)  
                .WithMany()  
                .HasForeignKey(pcad => pcad.ProductCategoryId)  
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<ProductCategoryAttributeData>()
                .HasOne(pcad => pcad.Attribute) 
                .WithMany() 
                .HasForeignKey(pcad => pcad.AttributeId)  
                .OnDelete(DeleteBehavior.Cascade);
            // ProductCategoryData
            modelBuilder.Entity<ProductCategoryData>()
                .HasOne(pcd => pcd.Product)  
                .WithMany()  
                .HasForeignKey(pcd => pcd.ProductId)  
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<ProductCategoryData>()
                .HasOne(pcd => pcd.ProductCategory)  
                .WithMany() 
                .HasForeignKey(pcd => pcd.ProductCategoryId) 
                .OnDelete(DeleteBehavior.Restrict);
            // ProductVariant
            modelBuilder.Entity<ProductVariant>()
                .HasOne(pv => pv.Product)  
                .WithMany()  
                .HasForeignKey(pv => pv.ProductId)  
                .OnDelete(DeleteBehavior.Restrict);

            // ProductVideo
            modelBuilder.Entity<ProductVideo>()
                .HasOne(p => p.Product) 
                .WithMany() 
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade); 



        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-R23TMQS\\ALEXNGUYEN;uid=sa;pwd=12345;Database=EcommerceFashion;Trusted_Connection=True;TrustServerCertificate=True");
        //}
    }
}
