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
    [Table("ProductVariantAttributeValueDatas")]
    public class ProductVariantAttributeValueData : BaseEntity
    {
        public required Guid ProductVariantId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        public ProductVariant ProductVariant { get; set; } = null!;
        public required Guid ProductAttributeValueId { get; set; }

        [ForeignKey(nameof(ProductAttributeValueId))]
        public ProductAttributeValue ProductAttributeValue { get; set; } = null!;
        public ICollection<OrderDetailAttribute> OrderDetailAttributes { get; set; } = new List<OrderDetailAttribute>();
        public ICollection<WishListAttribute> WishListAttributes { get; set; } = new List<WishListAttribute>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

}
