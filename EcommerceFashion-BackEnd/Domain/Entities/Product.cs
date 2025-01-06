using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public required string AvatarPath { get; set; }
        public required string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailDescription { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal SalePrice { get; set; }
        public float? Weight { get; set; }
        public float? Length { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
        public int? Status { get; set; }
        public string? SKU { get; set; }
        public Guid? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public Account Shop { get; set; } = null!;
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductCategoryData> ProductCategoryDatas { get; set; } = new List<ProductCategoryData>();
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
        public ICollection<ProductVideo> ProductVideos { get; set; } = new List<ProductVideo>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }

}
