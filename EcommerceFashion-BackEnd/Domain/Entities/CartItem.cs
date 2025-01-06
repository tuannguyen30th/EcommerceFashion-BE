using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    [Table("CartItems")]
    public class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;
        public required Guid ProductVariantAttributeValueDataId { get; set; }

        [ForeignKey(nameof(ProductVariantAttributeValueDataId))]
        public ProductVariantAttributeValueData ProductVariantAttributeValueData { get; set; } = null!;
    }

}
