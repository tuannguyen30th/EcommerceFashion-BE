using AutoMapper.Features;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class InitialSeeding
    {
        private static readonly List<Role> Roles = new()
    {
        new() { Name = Domain.Enums.Role.Admin.ToString() },
        new() { Name = Domain.Enums.Role.User.ToString() },
        new() { Name = Domain.Enums.Role.Shop.ToString() }
    };


        private static readonly List<Brand> Brands = new()
        {
            new() {Id = Guid.Parse("5BD317C9-9B48-4239-BAB0-0183628065FB"), Name = "Brand A", VitualPath = "/brands/brand-a", CoverImage = "brand-a.jpg", Description = "Description for Brand A" },
            new() {Id = Guid.Parse("6565B175-3428-4931-A08D-09D6913C6B60"), Name = "Brand B", VitualPath = "/brands/brand-b", CoverImage = "brand-b.jpg", Description = "Description for Brand B" }
        };

        private static readonly List<ProductCategory> Categories = new()
        {
            new() {Id = Guid.Parse("5BD317C9-9B48-4239-BAB0-0183628064FB"), Name = "Electronics", VirtualPath = "/categories/electronics" },
            new() {Id = Guid.Parse("5BD317C9-9B48-4239-BAB0-0183628063FB"), Name = "Fashion", VirtualPath = "/categories/fashion" }
        };

        private static readonly List<Product> Products = new()
        {
            new()
            {
                Id = Guid.Parse("5BD317C9-9B48-4239-BAB0-0183628062FB"),
                AvatarPath = "/products/product1.jpg",
                Name = "Smartphone",
                ShortDescription = "A high-quality smartphone",
                DefaultPrice = 699.99M,
                SalePrice = 599.99M,
                Weight = 0.5F,
                Length = 15.0F,
                Width = 7.5F,
                Height = 0.8F,
                Status = 1,
                SKU = "SMARTPHONE-001",
                CreatedById = Guid.Parse("31E944F2-B651-4CFD-EF0B-08DD2BB9975C"),
                BrandId = Brands[0].Id
            },
            new()
            {
                Id = Guid.Parse("5BD317C9-9B48-4239-BAB0-0183628061FB"),
                AvatarPath = "/products/product2.jpg",
                Name = "Laptop",
                ShortDescription = "A powerful gaming laptop",
                DefaultPrice = 1299.99M,
                SalePrice = 1199.99M,
                Weight = 2.5F,
                Length = 35.0F,
                Width = 25.0F,
                Height = 2.0F,
                Status = 1,
                SKU = "LAPTOP-001",
                CreatedById = Guid.Parse("31E944F2-B651-4CFD-EF0B-08DD2BB9975C"),
                BrandId = Brands[0].Id
            }
            ,
            new()
            {
                Id = Guid.Parse("5BD316C9-9B48-4239-BAB0-0183628061FB"),
                AvatarPath = "/products/product2.jpg",
                Name = "Clothes",
                ShortDescription = "A Short",
                DefaultPrice = 1299.99M,
                SalePrice = 1199.99M,
                Status = 1,
                SKU = "CLOTHES-001",
                CreatedById = Guid.Parse("31E944F2-B651-4CFD-EF0B-08DD2BB9975C"),
                BrandId = Brands[0].Id
            }
        };

        private static readonly List<ProductAttribute> Attributes = new()
        {
            new() {   Id = Guid.Parse("5BD317C9-9B48-4238-BAB0-0183628061FB"), Name = "Color", Type = 1, LimitNumberValue = 5 },
            new() {   Id = Guid.Parse("5BD317C9-9B48-4237-BAB0-0183628061FB"), Name = "Size", Type = 1, LimitNumberValue = 10 }
        };

        private static readonly List<ProductAttributeValue> AttributeValues = new()
        {
            new() {Id = Guid.Parse("5BD317C9-9B48-4236-BAB0-0183628061FB"), AttributeId = Attributes[0].Id, Value = "Red", Order = 1 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4235-BAB0-0183628061FB"), AttributeId = Attributes[0].Id, Value = "Blue", Order = 2 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4234-BAB0-0183628061FB"), AttributeId = Attributes[1].Id, Value = "XL", Order = 2 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4233-BAB0-0183628061FB"), AttributeId = Attributes[1].Id, Value = "L", Order = 2 },
        };

        private static readonly List<ProductVariant> Variants = new()
        {
            new() {Id = Guid.Parse("5BD317C9-9B48-4232-BAB0-0183628061FB"), ProductId = Products[0].Id, Stock = 100 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4231-BAB0-0183628061FB"), ProductId = Products[0].Id, Stock = 200 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4229-BAB0-0183628061FB"), ProductId = Products[0].Id, Stock = 200 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4228-BAB0-0183628061FB"), ProductId = Products[1].Id, Stock = 200 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4227-BAB0-0183628061FB"), ProductId = Products[1].Id, Stock = 200 },
            new() {Id = Guid.Parse("5BD317C9-9B48-4226-BAB0-0183628061FB"), ProductId = Products[1].Id, Stock = 200 },
        };

        private static readonly List<BrandProductCategoryData> BrandCategoryLinks = new()
        {
            new() { Id = Guid.Parse("5BD317C9-9B48-4225-BAB0-0183628061FB"), BrandId = Brands[0].Id, ProductCategoryId = Categories[0].Id, ProductCategoryOrder = 1 }
        };

        private static readonly List<ProductCategoryData> ProductCategoryDatas = new()
        {
            new() {Id = Guid.Parse("5BD317C9-9B48-4224-BAB0-0183628061FB"), ProductId = Products[0].Id, ProductCategoryId = Categories[0].Id },
            new() {Id = Guid.Parse("5BD317C8-9B48-4223-BAB0-0183628061FB"), ProductId = Products[1].Id, ProductCategoryId = Categories[1].Id },
            new() {Id = Guid.Parse("5BD317C7-9B48-4222-BAB0-0183628061FB"), ProductId = Products[2].Id, ProductCategoryId = Categories[0].Id },
        };

        private static readonly List<ProductCategoryAttributeData> CategoryAttributes = new()
        {
            new() { Id = Guid.Parse("5BD417C7-9B48-4221-BAB0-0183628061FB"), ProductCategoryId = Categories[1].Id, AttributeId = Attributes[0].Id, AttributeOrder = 1 },
            new() { Id = Guid.Parse("5BD417C7-9B48-4220-BAB0-0183628061FB"), ProductCategoryId = Categories[1].Id, AttributeId = Attributes[1].Id, AttributeOrder = 2 },
            new() { Id = Guid.Parse("5BD417C7-9B48-4219-BAB0-0183628061FB"), ProductCategoryId = Categories[0].Id, AttributeId = Attributes[0].Id, AttributeOrder = 1 },
            new() { Id = Guid.Parse("5BD417C7-9B48-4218-BAB0-0183628061FB"), ProductCategoryId = Categories[0].Id, AttributeId = Attributes[1].Id, AttributeOrder = 2 },
        };
        private static readonly List<ProductVariantAttributeValueData> ProductVariantAttributeValueDatas = new()
        {
            new() { Id = Guid.Parse("5BD417C7-9B48-4217-BAB0-0183628061FB"), ProductVariantId = Variants[0].Id, ProductAttributeValueId = AttributeValues[0].Id },
            new() { Id = Guid.Parse("5BD417C7-9B48-4216-BAB0-0183628061FB") ,ProductVariantId = Variants[0].Id, ProductAttributeValueId = AttributeValues[2].Id},
            new() { Id = Guid.Parse("5BD417C7-9B48-4215-BAB0-0183628061FB"), ProductVariantId = Variants[1].Id, ProductAttributeValueId = AttributeValues[1].Id },
            new() { Id = Guid.Parse("5BD417C7-9B48-4214-BAB0-0183628061FB"), ProductVariantId = Variants[1].Id, ProductAttributeValueId = AttributeValues[3].Id },
            new() { Id = Guid.Parse("5BD417C7-9B48-4213-BAB0-0183628061FB"), ProductVariantId = Variants[2].Id, ProductAttributeValueId = AttributeValues[1].Id },
            new() { Id = Guid.Parse("5BD417C7-9B48-4211-BAB0-0183628061FB"), ProductVariantId = Variants[2].Id, ProductAttributeValueId = AttributeValues[2].Id },
        };

        /// <summary>
        /// Initialize and seed the database with roles, categories, and subcategories.
        /// </summary>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EcommerceFashionDbContext>();

            // Seed Roles
            foreach (var role in Roles)
            {
                if (!context.Roles.Any(r => r.Name == role.Name))
                {
                    role.CreationDate = DateTime.UtcNow;
                    context.Roles.Add(role);
                }
            }
            foreach (var brand in Brands)
            {
                if (!context.Brands.Any(b => b.Name == brand.Name))
                {
                    brand.CreationDate = DateTime.UtcNow;
                    context.Brands.Add(brand);
                }
            }

            // Seed Categories
            foreach (var category in Categories)
            {
                if (!context.ProductCategories.Any(c => c.Name == category.Name))
                {
                    category.CreationDate = DateTime.UtcNow;
                    context.ProductCategories.Add(category);
                }
            }

            // Seed Products
            foreach (var product in Products)
            {
                if (!context.Products.Any(p => p.Name == product.Name))
                {
                    product.CreationDate = DateTime.UtcNow;
                    context.Products.Add(product);
                }
            }

            // Seed Attributes
            foreach (var attribute in Attributes)
            {
                if (!context.Attributes.Any(a => a.Name == attribute.Name))
                {
                    attribute.CreationDate = DateTime.UtcNow;
                    context.Attributes.Add(attribute);
                }
            }

            // Seed Attribute Values
            foreach (var value in AttributeValues)
            {
                if (!context.ProductAttributeValues.Any(av => av.Value == value.Value))
                {
                    value.CreationDate = DateTime.UtcNow;
                    context.ProductAttributeValues.Add(value);
                }
            }

            // Seed Variants
            foreach (var variant in Variants)
            {
                if (!context.ProductVariants.Any(v => v.ProductId == variant.ProductId))
                {
                    variant.CreationDate = DateTime.UtcNow;
                    context.ProductVariants.Add(variant);
                }
            }

            // Seed Relationships
            foreach (var brandCategoryLink in BrandCategoryLinks)
            {
                if (!context.BrandProductCategoryDatas.Any(bc => bc.BrandId == brandCategoryLink.BrandId && bc.ProductCategoryId == brandCategoryLink.ProductCategoryId))
                {
                    brandCategoryLink.CreationDate = DateTime.UtcNow;
                    context.BrandProductCategoryDatas.Add(brandCategoryLink);
                }
            }

            foreach (var productCategoryData in ProductCategoryDatas)
            {
                if (!context.ProductCategoryDatas.Any(pc => pc.ProductId == productCategoryData.ProductId && pc.ProductCategoryId == productCategoryData.ProductCategoryId))
                {
                    productCategoryData.CreationDate = DateTime.UtcNow;
                    context.ProductCategoryDatas.Add(productCategoryData);
                }
            }

            foreach (var categoryAttribute in CategoryAttributes)
            {
                if (!context.ProductCategoryAttributeDatas.Any(ca => ca.ProductCategoryId == categoryAttribute.ProductCategoryId && ca.AttributeId == categoryAttribute.AttributeId))
                {
                    categoryAttribute.CreationDate = DateTime.UtcNow;
                    context.ProductCategoryAttributeDatas.Add(categoryAttribute);
                }
            }

            foreach (var productVariantAttributeValueData in ProductVariantAttributeValueDatas)
            {
                // Check for duplicates in ProductVariantAttributeValueDatas, not ProductCategoryAttributeDatas
                if (!context.ProductVariantAttributeValueDatas.Any(pvav => pvav.Id == productVariantAttributeValueData.Id))
                {
                    productVariantAttributeValueData.CreationDate = DateTime.UtcNow;
                    context.ProductVariantAttributeValueDatas.Add(productVariantAttributeValueData);
                }
            }


            await context.SaveChangesAsync();
        }
    }
}
