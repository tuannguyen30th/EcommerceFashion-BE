/*using Application.FeedbackService.Models;
using Application.FeedbackService.Queries;
using Application.ProductService.Models;
using Application.ProductService.Models.RequestModels;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductService.Queries
{
    public record GetTopSellingProductsQuery(ProductFilterModel productFilterModel) : IRequest<ResponseModel>;

    public class GetTopSellingProductsQueryHandler : IRequestHandler<GetTopSellingProductsQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public GetTopSellingProductsQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ResponseModel> Handle(GetTopSellingProductsQuery request, CancellationToken cancellationToken)
        {
            var orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();

            var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync(
               pageIndex: request.productFilterModel.PageIndex,
               pageSize: request.productFilterModel.PageSize,
               filter: _ => (_.Order.PaymentStatus.Equals(PaymentStatus.Completed)),
               include: "Order.Payment,ProductVariantAttributeValue.ProductVariant,ProductVariantAttributeValue.ProductVariant.Images"
            );

            var groupedProducts = orderDetails.Data
                .GroupBy(_ => new
                {
                    _.ProductVariantAttributeValue.ProductVariant.Id,
                    _.ProductVariantAttributeValue.ProductVariant.Product.Name,
                    _.ProductVariantAttributeValue.ProductVariant.Product.DefaultPrice,
                    _.ProductVariantAttributeValue.ProductVariant.Product.SalePrice,
                    _.ProductVariantAttributeValue.ProductVariant.Stock
                })
                .Where(_ => _.Key.Stock > 0)
                .Select(_ => new
                {
                    ProductId = _.Key.Id,
                    ProductName = _.Key.Name,
                    DefaultPrice = _.Key.DefaultPrice,
                    SalePrice = _.Key.SalePrice,
                    TotalSold = _.Sum(od => od.Count),
                    AvatarPath = _.Select(od => od.ProductVariantAttributeValue.ProductVariant.Product.Images.FirstOrDefault().ImagePath).FirstOrDefault()
                })
                .OrderByDescending(_ => _.TotalSold)
                .Take(30)
                .ToList();

            var topSellingProducts = new List<GeneralProductModel>();

            foreach (var product in groupedProducts)
            {
                var feedbackResponse = await _mediator.Send(new GetAllFeedbacksByProductIdQuery(product.ProductId));
                var feedbacks = feedbackResponse?.Data as IEnumerable<GetFeedbacksModel> ?? Enumerable.Empty<GetFeedbacksModel>();
                var feedbackCount = feedbacks.Count();
                var totalRating = feedbacks.Sum(_ => _.Rating);

                var averageRating = feedbackCount > 0 ? (float)totalRating / feedbackCount : 0;

                var sellingProduct = new GeneralProductModel
                {
                    Id = product.ProductId,
                    Name = product.ProductName,
                    AvatarPath = product.AvatarPath,
                    DefaultPrice = product.DefaultPrice,
                    SalePrice = product.SalePrice > 0 ? product.SalePrice : 0,
                    Discount = product.SalePrice > 0
                        ? (float)((product.DefaultPrice - product.SalePrice) / product.DefaultPrice) * 100
                        : 0,
                    AverageRating = averageRating,
                    IsTop = true
                };

                topSellingProducts.Add(sellingProduct);
            }

            return new Pagination<GeneralProductModel>(
                topSellingProducts,
                request.productFilterModel.PageIndex,
                request.productFilterModel.PageSize,
                orderDetails.TotalCount
            );
        }
    }
}
*/