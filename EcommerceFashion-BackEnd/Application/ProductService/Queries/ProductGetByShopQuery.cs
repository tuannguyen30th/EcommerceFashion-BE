using Application.FeedbackService.Models;
using Application.FeedbackService.Models.ResponseModels;
using Application.FeedbackService.Queries;
using Application.ProductService.Models;
using Application.ProductService.Models.RequestModels;
using Application.ProductService.Models.ResponseModels;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductService.Queries
{
    public record ProductGetByShopQuery(Guid shopId, ProductFilterModel productFilterModel) : IRequest<ResponseModel>;
    public class ProductGetByShopQueryHandler : IRequestHandler<ProductGetByShopQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductGetByShopQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<ResponseModel> Handle(ProductGetByShopQuery request, CancellationToken cancellationToken)
        {
            var requestModel = request.productFilterModel;
            var productLists = await _unitOfWork.ProductRepository.GetAllAsync(
                pageIndex: requestModel.PageIndex,
                pageSize: requestModel.PageSize,
                filter: _ => (_.CreatedById == request.shopId && _.IsDeleted == false)
            );

            var productOfShops = new List<ProductModel>();

            foreach (var product in productLists.Data)
            {
                var feedbackResponse = await _mediator.Send(new FeedbackGetByProductQuery(product.Id));
                var feedbacks = feedbackResponse?.Data as IEnumerable<FeedbackModel> ?? Enumerable.Empty<FeedbackModel>();
                var feedbackCount = feedbacks.Count();
                var totalRating = feedbacks.Sum(_ => _.Rating);

                var averageRating = feedbackCount > 0 ? (float)totalRating / feedbackCount : 0;

                var productShop = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    AvatarPath = product.AvatarPath,
                    DefaultPrice = product.DefaultPrice,
                    SalePrice = product.SalePrice > 0 ? product.SalePrice : 0,
                    Discount = product.SalePrice > 0
                        ? (float)((product.DefaultPrice - product.SalePrice) / product.DefaultPrice) * 100
                        : 0,
                    AverageRating = averageRating,
                };

                productOfShops.Add(productShop);
            }
            var result = new Pagination<ProductModel>(productOfShops, requestModel.PageIndex,
               requestModel.PageSize, productLists.TotalCount);
            return new ResponseModel
            {
                Data = result
            };
        }
    }
}
