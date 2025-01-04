using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory : BaseEntity
    {
        public required string Name { get; set; } 
        public string? VirtualPath { get; set; } 
        public Guid? ParentId { get; set; }
        public ProductCategory? Parent { get; set; } 
        public ICollection<BrandProductCategoryData> BrandProductCategoryDatas { get; set; } = new List<BrandProductCategoryData>();    
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<ProductCategoryAttributeData> ProductCategoryAttributeDatas { get; set; } = new List<ProductCategoryAttributeData>();    
        public ICollection<ProductCategoryData> ProductCategoryDatas { get; set; } = new List<ProductCategoryData>();


    }
}
