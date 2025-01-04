using Application.FeedbackService.Models;
using Application.FeedbackService.Models.ResponseModels;
using Application.FeedbackService.Queries;
using Application.ProductService.Models;
using Application.ProductService.Models.RequestModels;
using Application.ProductService.Models.ResponseModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductService.Queries
{
    public record ProductGetSaleQuery(ProductFilterModel productFilterModel) : IRequest<ResponseModel>;

    public class ProductGetSaleQueryHandler : IRequestHandler<ProductGetSaleQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductGetSaleQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ResponseModel> Handle(ProductGetSaleQuery request, CancellationToken cancellationToken)
        {
            var requestModel = request.productFilterModel;
            var saleProductLists = await _unitOfWork.ProductRepository.GetAllAsync(
                pageIndex: requestModel.PageIndex,
                pageSize: requestModel.PageSize,
                filter: _ => (_.SalePrice > 0 && _.IsDeleted == requestModel.IsDeleted && _.ProductVariants.Sum(_ => _.Stock) > 0),
                include: saleProductLists => saleProductLists.Include(_ => _.Images).Include(_ => _.Feedbacks)
            );

            var saleProductModels = await Task.WhenAll(saleProductLists.Data.Select(async _ =>
            {
                var feedbackResponse = await _mediator.Send(new FeedbackGetByProductQuery(_.Id));
                var feedbacks = feedbackResponse?.Data as IEnumerable<FeedbackModel> ?? Enumerable.Empty<FeedbackModel>();
                var feedbackCount = feedbacks.Count();
                var totalRating = feedbacks.Sum(_ => _.Rating);
                var averageRating = feedbackCount > 0 ? (float)totalRating / feedbackCount : 0;

                return new ProductModel
                {
                    Id = _.Id,
                    Name = _.Name,
                    AvatarPath = _.Images.FirstOrDefault()?.ToString() ?? string.Empty,
                    DefaultPrice = _.DefaultPrice,
                    SalePrice = _.SalePrice > 0 ? _.SalePrice : 0,
                    Discount = _.SalePrice > 0
                        ? (float)((_.DefaultPrice - _.SalePrice) / _.DefaultPrice) * 100
                        : 0,
                    AverageRating = averageRating,
                    IsSale = true
                };
            }));

            var resolvedProducts = saleProductModels.ToList();
            var result = new Pagination<ProductModel>(resolvedProducts, requestModel.PageIndex,
               requestModel.PageSize, saleProductLists.TotalCount);
            return new ResponseModel
            {
                Data = result
            };
          
        }
    }
}
