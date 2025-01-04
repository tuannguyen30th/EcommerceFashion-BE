using Application.FeedbackService.Models;
using Application.FeedbackService.Models.ResponseModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedbackService.Queries
{

    public record FeedbackGetByShopQuery(Guid shopId) : IRequest<ResponseModel>;
    public class FeedbackGetByShopQueryHandler : IRequestHandler<FeedbackGetByShopQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackGetByShopQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(FeedbackGetByShopQuery request, CancellationToken cancellationToken)
        {
            var feedbackList = await _unitOfWork.FeedbackRepository.GetAllAsync(
                filter: _ => (_.ShopId == request.shopId),
                include: _ => _.Include("Product,Images")
                );
            var feedbackModel = feedbackList?.Data?.Select(_ => new FeedbackModel
            {
                Id = _.Id,
                AuthorName = _.Account.FirstName + " " + _.Account.LastName,
                CreatedById = _.CreatedById,
                ShopId = _.ShopId,
                Date = _.CreationDate,
                Description = _.Description,
                Rating = _.Rating,
            }).ToList();
            return new ResponseModel { Data = feedbackModel};
        }
    }
}
