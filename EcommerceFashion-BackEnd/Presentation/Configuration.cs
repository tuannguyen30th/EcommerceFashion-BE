using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Middlewares;
using System.Diagnostics;
using System.Text;
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.AccountService.Commands;
using System.Reflection;
using Infrastructure.Common;
using Application.BrandService.Queries;
using Application.BrandService.Commands;
using Application.CartService.Commands;
using Application.CartService.Queriess;
using Application.FeedbackService.Commands;
using Application.FeedbackService.Queries;
using Application.ProductCategoryService.Queries;
using Application.ProductService.Queries;
using Application.ShopService.Queries;
using Application.WishListService.Commands;
using Application.WishListService.Queries;

namespace Presentation
{
    public static class Configuration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            #region Configuartion

            // Local database
            services.AddDbContextPool<EcommerceFashionDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LocalDb"));
            });

            // Redis
            //var redisConfiguration = configuration["Redis:Configuration"];
            //ArgumentException.ThrowIfNullOrWhiteSpace(redisConfiguration);
            //services.AddStackExchangeRedisCache(options => { options.Configuration = redisConfiguration; });
            //services.AddSingleton<IConnectionMultiplexer>(
            //    ConnectionMultiplexer.Connect(redisConfiguration));

            // Cloudinary
            //var cloud = configuration["Cloudinary:Cloud"];
            //ArgumentException.ThrowIfNullOrWhiteSpace(cloud);
            //var apiKey = configuration["Cloudinary:ApiKey"];
            //ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
            //var apiSecret = configuration["Cloudinary:ApiSecret"];
            //ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
            //var cloudinary = new Cloudinary(new Account { Cloud = cloud, ApiKey = apiKey, ApiSecret = apiSecret });
            //services.AddSingleton<ICloudinary>(cloudinary);

            // JWT
            var secret = configuration["JWT:Secret"];
            ArgumentException.ThrowIfNullOrWhiteSpace(secret);
            var issuer = configuration["JWT:ValidIssuer"];
            ArgumentException.ThrowIfNullOrWhiteSpace(issuer);
            var audience = configuration["JWT:ValidAudience"];
            ArgumentException.ThrowIfNullOrWhiteSpace(audience);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrWhiteSpace(accessToken) && path.StartsWithSegments("/hub"))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });

            

            // CORS
            var clientUrl = configuration["URL:Client"];
            ArgumentException.ThrowIfNullOrWhiteSpace(clientUrl);
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder
                            .WithOrigins(clientUrl)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            #endregion

            #region Middleware

            services.AddScoped<AccountStatusMiddleware>();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();

            #endregion

            #region Common

            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Helper

            //services.AddScoped<ICloudinaryHelper, CloudinaryHelper>();
            //services.AddTransient<IEmailHelper, EmailHelper>();
            //services.AddScoped<IRedisHelper, RedisHelper>();

            #endregion

            #region Dependency Injection

            // Account

            services.AddScoped<IAccountRepository, AccountRepository>();

            // Role
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Brand
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandProductCategoryDataRepository, BrandProductCategoryDataRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICreditHistoryRepository, CreditHistoryRepository>();
            services.AddScoped<ICreditRepository, CreditRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IOrderDetailAttributeRepository, OrderDetailAttributeRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeValueRepository, ProductAttributeValueRepository>();
            services.AddScoped<IProductCategoryAttributeDataRepository, ProductCategoryAttributeDataRepository>();
            services.AddScoped<IProductCategoryDataRepository, ProductCategoryDataRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantAttributeValueDataRepository, ProductVariantAttributeValueDataRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IProductVideoRepository, ProductVideoRepository>();
            services.AddScoped<IWishListAttributeRepository, WishListAttributeRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();

            #endregion

            #region MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SendEmailCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AccountSignUpCommandHandler).GetTypeInfo().Assembly));
            // Brand
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BrandGetAllQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BrandAddCommandHandler).GetTypeInfo().Assembly));

            // Cart
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CartAddCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CartUpdateQuantityCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CartGetByAccountQueryHandler).GetTypeInfo().Assembly));

            // Feedback
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(FeedbackAddForShopOrWebsiteCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(FeedbackGetByProductQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(FeedbackGetByShopQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(FeedbackGetForWebsiteQueryHandler).GetTypeInfo().Assembly));

            // ProductCategory
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ProductCategoryGetQueryHandler).GetTypeInfo().Assembly));

            // Product
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ProductGetByShopQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ProductGetNewQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ProductGetSaleQueryHandler).GetTypeInfo().Assembly));

            // Shop
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ShopGetDetailQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ShopGetAllQueryHandler).GetTypeInfo().Assembly));

            // WishList
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(WishListAddOrDeleteCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(WishListGetByAccountQueryHandler).GetTypeInfo().Assembly));

            #endregion
            return services;
        }
    }
}
