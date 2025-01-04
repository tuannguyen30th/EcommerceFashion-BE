using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WishListService.Models
{
    public class GetAllWishListsByAccountModel
    {
        public Guid Id { get; set; }
        public string AvatarPath { get; set; }
        public string Name { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public float? Discount { get; set; }
        public List<ProductAttributeListModel> ProductAttributeLists  { get; set; } = new List<ProductAttributeListModel>();

    }
    public class ProductAttributeListModel
    {
        public Guid Id { get; set; }
        public Guid WishListAttributeId { get; set; }
        public string Name { get; set; }
    }
}
