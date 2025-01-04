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
    [Table("ProductCategoryAttributeDatas")]
    public class ProductCategoryAttributeData : BaseEntity
    {

        public required Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
        public required Guid AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; } = null!;
        public int AttributeOrder { get; set; }
    }
}
