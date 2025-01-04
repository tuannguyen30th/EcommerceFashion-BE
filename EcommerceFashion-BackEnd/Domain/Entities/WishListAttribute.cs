using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("WishListAttributes")]
    public class WishListAttribute : BaseEntity
    {
        public Guid WishListId { get; set; }
        public WishList WishList { get; set; } = null!;
        public required Guid ProductVariantAttributeValueDataId { get; set; }
        public ProductVariantAttributeValueData ProductVariantAttributeValueData { get; set; } = null!;
    }
}
