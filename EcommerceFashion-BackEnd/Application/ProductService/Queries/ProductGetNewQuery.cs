using Application.FeedbackService.Models;
using Application.FeedbackService.Models.ResponseModels;
using Application.FeedbackService.Queries;
using Application.ProductService.Models;
using Application.ProductService.Models.ResponseModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductService.Queries
{
    public record ProductGetNewQuery : IRequest<ResponseModel>;

    public class ProductGetNewQueryHandler : IRequestHandler<ProductGetNewQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductGetNewQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ResponseModel> Handle(ProductGetNewQuery request, CancellationToken cancellationToken)
        {

            var newProductLists = await _unitOfWork.ProductRepository.GetAllAsync(
                filter: _ => (_.CreationDate >= DateTime.UtcNow.AddDays(-30) && _.ProductVariants.Sum(_ => _.Stock) > 0 && _.IsDeleted == false),
                include: newProductList => newProductList.Include(_ => _.Images) .Include(_ => _.Feedbacks)
            );

            if (newProductLists?.Data == null || !newProductLists.Data.Any())
                return new ResponseModel { Data = null, Code = StatusCodes.Status404NotFound};

            var products = newProductLists.Data.Select(async _ =>
            {
                var feedbackResponse = await _mediator.Send(new FeedbackGetByProductQuery(_.Id));
                var feedBacks = feedbackResponse?.Data as IEnumerable<FeedbackModel>;
                var feedbackCount = feedBacks.Count();
                var totalRating = feedBacks.Sum(_ => _.Rating);
                var averageRating = feedbackCount > 0 ? (float)totalRating / feedbackCount : 0;

                return new ProductModel
                {
                    Id = _.Id,
                    Name = _.Name,
                    AvatarPath = _.Images.FirstOrDefault().ToString(),
                    DefaultPrice = _.DefaultPrice,
                    SalePrice = _.SalePrice > 0 ? _.SalePrice : 0,
                    Discount = _.SalePrice > 0
                        ? (float)((_.DefaultPrice - _.SalePrice) / _.DefaultPrice) * 100
                        : 0,
                    AverageRating = averageRating,
                    IsArrival = true
                };
            });

            var resolvedProducts = await Task.WhenAll(products);

            return new ResponseModel { Data = resolvedProducts.ToList() ?? null };
        }
    }
}
