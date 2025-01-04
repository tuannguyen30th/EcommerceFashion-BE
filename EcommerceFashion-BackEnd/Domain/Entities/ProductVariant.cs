using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ProductVariants")]
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public required int Stock {  get; set; }
        public ICollection<ProductVariantAttributeValueData> ProductVariantAttributeValueDatas { get; set; } = new List<ProductVariantAttributeValueData>();

    }
}
