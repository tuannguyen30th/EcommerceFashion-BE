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
        [ForeignKey(nameof(WishListId))]
        public WishList WishList { get; set; } = null!;
        public Guid ProductVariantAttributeValueDataId { get; set; }

        [ForeignKey(nameof(ProductVariantAttributeValueDataId))]
        public ProductVariantAttributeValueData ProductVariantAttributeValueData { get; set; } = null!;
    }

}
