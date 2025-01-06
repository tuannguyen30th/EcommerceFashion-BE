using Application.FeedbackService.Models.ResponseModels;
using Application.ProductService.Models.ResponseModels;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;

namespace Application.ProductService.Queries
{
    public record ProductGetDetailQuery(Guid Id) : IRequest<ResponseModel>;
    public class ProductGetDetailQueryHandler : IRequestHandler<ProductGetDetailQuery, ResponseModel>
    {
        private readonly IUnitOfWork? _unitOfWork;
        public ProductGetDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel> Handle(ProductGetDetailQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(
                request.Id
     //request.Id,
     //query => query
     //    .Include(p => p.ProductVariants)
     //        .ThenInclude(v => v.ProductVariantAttributeValueDatas)
     //            .ThenInclude(vav => vav.ProductAttributeValue)
     //                .ThenInclude(pa => pa.Attribute)
     //    .Include(p => p.Feedbacks)
     //    .Include(p => p.Images)
 );

            if (product == null)
            {
                return new ResponseModel { Code = StatusCodes.Status404NotFound ,Message = "Product not found" };
            }

            var groupedAttributes = product.ProductVariants
                                           .SelectMany(v => v.ProductVariantAttributeValueDatas)
                                           .GroupBy(
                                           avd => avd.ProductAttributeValue.Attribute.Name, 
                                           avd => new
                                           {
                                               avd.ProductAttributeValue.Id,
                                               Value = avd.ProductAttributeValue.Value
                                           }
                                           )
                                           .ToDictionary(g => g.Key, g => g.ToList());
            var stock = await _unitOfWork.ProductVariantRepository.Stock(request.Id);

            var response = new ProductDetailModel
            {
                Id = product.Id,
                Name = product.Name,
                //ShopId = product.ShopId.ToString(),
                //ShopName = product.Shop.ShopName,
                AverageRating = (float)(product.Feedbacks.Any() ? product.Feedbacks.Average(f => f.Rating) : 0),
                DefaultPrice = product.DefaultPrice,
                SalePrice = product.SalePrice,
                Percent = product.DefaultPrice > 0
                    ? (float)((product.DefaultPrice - product.SalePrice) / product.DefaultPrice) * 100
                    : 0,
                Description = product.DetailDescription ?? string.Empty,
                Stock = stock,
                TotalReview = product.Feedbacks.Count,
                Images = product.Images.Select(img => img.ImagePath).ToList(),
                FeedbackModels = product.Feedbacks.Select(f => new FeedbackModel
                {
                    Id = f.Id,
                    Rating = f.Rating,
                    Description = f.Description,
                    Images = f.Images?.Select(img => img.ImagePath).ToList() ?? new List<string>()
                }).ToList(),
                ProductDetailAttributeModels = groupedAttributes.Select(attr => new ProductDetailAttributeModel
                {
                    Id = attr.Value.First().Id,
                    Value = attr.Key 
                }).ToList()
            };

            return new ResponseModel { Data = response };
        }
    }
}
