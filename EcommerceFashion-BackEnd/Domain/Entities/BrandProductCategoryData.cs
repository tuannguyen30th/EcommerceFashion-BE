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
    [Table("BrandProductCategoryDatas")]
    public class BrandProductCategoryData : BaseEntity
    {
        public required Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public required Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
        public int ProductCategoryOrder {  get; set; }
    }
}
