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
    [Table("ProductCategoryDatas")]
    public class ProductCategoryData : BaseEntity
    {
        public required Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public required Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
       /* public int IntOrder { get; set; }*/

    }
}
