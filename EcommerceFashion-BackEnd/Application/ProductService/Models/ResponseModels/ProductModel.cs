using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductService.Models.ResponseModels
{
    public class ProductModel : BaseEntity
    {
        public string AvatarPath { get; set; }
        public string Name { get; set; }
        public float AverageRating { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal SalePrice { get; set; }
        public float Discount { get; set; }
        public bool? IsArrival { get; set; }
        public bool? IsTop { get; set; }
        public bool? IsSale { get; set; }
        public string? Label { get; set; }

    }
}
