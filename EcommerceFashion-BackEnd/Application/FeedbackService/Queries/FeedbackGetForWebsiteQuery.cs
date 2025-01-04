using Application.FeedbackService.Models;
using Application.FeedbackService.Models.RequestModels;
using Application.FeedbackService.Models.ResponseModels;
using Application.ProductService.Models;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedbackService.Queries
{
    public record FeedbackGetForWebsiteQuery(FeedbackFilterModel feedbackFilterModel) : IRequest<ResponseModel>;
    public class FeedbackGetForWebsiteQueryHandler : IRequestHandler<FeedbackGetForWebsiteQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackGetForWebsiteQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(FeedbackGetForWebsiteQuery request, CancellationToken cancellationToken)
        {
            var requestModel = request.feedbackFilterModel;
            var feedbackLists = await _unitOfWork.FeedbackRepository.GetAllAsync(
                filter: _ => (_.IsDeleted == request.feedbackFilterModel.IsDeleted && _.FeedbackType == Domain.Enums.FeedbackType.Website),
                include: feedbackList => feedbackList.Include(_ => _.Product) .Include(_ => _.Account) .Include(_ => _.Images),
                pageIndex: requestModel.PageIndex,
                pageSize: requestModel.PageSize
                );
            var feedbackModels = feedbackLists?.Data?.Select(_ => new FeedbackModel
            {
                Id = _.Id,
                CreatedById = _.CreatedById,
                AuthorName = _.Account.FirstName + " " + _.Account.LastName,
                Date = _.CreationDate,
                Description = _.Description,
                Rating = _.Rating
            }).ToList();
            var result = new Pagination<FeedbackModel>(feedbackModels, requestModel.PageIndex,
               requestModel.PageSize, feedbackLists.TotalCount);
            return new ResponseModel
            {
                Message = "Get all requests successfully",
                Data = result
            };
        }
    }
}
