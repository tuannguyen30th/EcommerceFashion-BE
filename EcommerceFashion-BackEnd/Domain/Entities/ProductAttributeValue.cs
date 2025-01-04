using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ProductAttributeValues")]
    public class ProductAttributeValue : BaseEntity
    {
        public Guid AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; } = null!;
        public string? Value { get; set; }
        public int Order { get; set; }
        public ICollection<ProductVariantAttributeValueData> ProductVariantAttributeValueDatas { get; set; } = new List<ProductVariantAttributeValueData >();

    }
}
