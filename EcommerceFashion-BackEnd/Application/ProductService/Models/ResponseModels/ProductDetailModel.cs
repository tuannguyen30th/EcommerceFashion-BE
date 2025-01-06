using Application.FeedbackService.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductService.Models.ResponseModels
{
    public class ProductDetailModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? ShopId { get; set; }
        public string? ShopName { get; set; }
        public float AverageRating { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal SalePrice { get; set; }
        public float Percent {  get; set; }
        public string Description { get; set; }
        public int Stock {  get; set; }
        public int TotalReview {  get; set; }
        public ICollection<ProductDetailAttributeModel> ProductDetailAttributeModels { get; set; } = new List<ProductDetailAttributeModel>();
        public ICollection<string> Images = new List<string>();
        public ICollection<FeedbackModel> FeedbackModels { get; set; } = new List<FeedbackModel>();

    }
    public class ProductDetailAttributeModel
    {
        public Guid Id {  set; get; }
        public required string Value { set; get; }
    }

}
