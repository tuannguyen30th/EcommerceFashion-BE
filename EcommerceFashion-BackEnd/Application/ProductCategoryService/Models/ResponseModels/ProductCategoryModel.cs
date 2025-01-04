using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductCategoryService.Models
{
    public class ProductCategoryModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? VirtualPath { get; set; }
    }
}
