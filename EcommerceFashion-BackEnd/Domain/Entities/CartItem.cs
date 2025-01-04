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
        public Cart Cart { get; set; } = null!;
        public required Guid ProductVariantAttributeValueDataId { get; set; }
        public ProductVariantAttributeValueData ProductVariantAttributeValueData { get; set; } = null!;

    }
}
