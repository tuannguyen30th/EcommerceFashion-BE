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
    [Table("Brands")]
    public class Brand : BaseEntity
    {
        public required string Name { get; set; }
        public required string VitualPath { get; set; }
        public required string CoverImage { get; set; }
        public required string Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<BrandProductCategoryData> BrandProductCategoryDatas { get; set; } = new List<BrandProductCategoryData>();

    }
}
