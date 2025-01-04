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
    public class ProductVariantAttributeValueData  : BaseEntity
    {
        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;
        public Guid ProductAttributeValueId { get; set; }
        public ProductAttributeValue ProductAttributeValue { get; set; } = null!;
        public ICollection<OrderDetailAttribute> OrderDetailAttributes { get; set; } = new List<OrderDetailAttribute>();
        /*public ICollection<Cart> Carts { get; set; } = new List<Cart>();*/
        public ICollection<WishListAttribute> WishListAttributes { get; set; } = new List<WishListAttribute>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
