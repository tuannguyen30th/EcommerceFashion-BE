using Application.WishListService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartService.Models.ResponseModels
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string AvatarPath { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal DefaultPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public float? Discount { get; set; }
        public int Quantity { get; set; }
        public List<ProductAttributesModel> ProductAttributeLists { get; set; } = new List<ProductAttributesModel>();
    }
    public class ProductAttributesModel
    {
        public Guid CartItemId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
