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
    public record FeedbackGetByProductQuery(Guid productId) : IRequest<ResponseModel>;
    public class FeedbackGetByProductQueryHandler : IRequestHandler<FeedbackGetByProductQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackGetByProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(FeedbackGetByProductQuery request, CancellationToken cancellationToken)
        {
            var feedbackList = await _unitOfWork.FeedbackRepository.GetAllAsync(
                filter: _ => (_.ProductId == request.productId),
                include: feedbackList => feedbackList.Include(_ => _.Product) .Include(_ => _.Images)
                );
            var feedbackModel = feedbackList?.Data?.Select(_ => new FeedbackModel
            {
                Id = _.Id,
                ProductId = _.ProductId,
                CreatedById = _.CreatedById,
                AuthorName = _.Account.FirstName + " " + _.Account.LastName,
                Date = _.CreationDate,
                Description = _.Description,
                Images = _.Images.Select(_ => _.ImagePath).ToArray(),
                Rating = _.Rating
            }).ToList();
            return new ResponseModel { Data = feedbackModel };
        }
    }
}
