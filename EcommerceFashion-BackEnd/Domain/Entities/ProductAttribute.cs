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
    [Table("ProductAttributes")]
    public class ProductAttribute : BaseEntity
    {
        public required string Name { get; set; }
        public required int Type { get; set; }
        public int LimitNumberValue { get; set; }
        public ICollection<AttributeSelection> AttributeSelections { get; set; } = new List<AttributeSelection>();
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
        public ICollection<ProductCategoryAttributeData> ProductCategoryAttributeDatas { get; set; } = new List<ProductCategoryAttributeData>();
    }
}
