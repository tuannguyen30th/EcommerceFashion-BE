using Application.FeedbackService.Models;
using Application.FeedbackService.Models.ResponseModels;
using Application.FeedbackService.Queries;
using Application.ShopService.Models;
using Application.ShopService.Models.ResponseModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ShopService.Queries
{
    public record ShopGetDetailQuery(Guid id) : IRequest<ResponseModel>;
    public class ShopGetDetailQueryHandler : IRequestHandler<ShopGetDetailQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;

        public ShopGetDetailQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _claimService = claimService;
        }

        public async Task<ResponseModel> Handle(ShopGetDetailQuery request, CancellationToken cancellationToken)
        {
            //var currentUserId = _claimService.GetCurrentUserId;
            //if (!currentUserId.HasValue)
            //{
            //    return new ResponseModel
            //    {
            //        Code = StatusCodes.Status401Unauthorized,
            //        Message = "Unauthorized."
            //    };
            //}
            var user = await _unitOfWork.AccountRepository.GetAsync(request.id);
            var feedbackResponse = await _mediator.Send(new FeedbackGetByShopQuery(request.id));
            var feedbacks = feedbackResponse?.Data as IEnumerable<FeedbackModel> ?? Enumerable.Empty<FeedbackModel>();

            var feedbackCount = feedbacks.Count();
            var totalRating = feedbacks.Sum(_ => _.Rating);
            var averageRating = feedbackCount > 0 ? (float)totalRating / feedbackCount : 0;
            var result = new ShopGetDetailModel
            {
                Id = user.Id,
                Name = user.ShopName ?? "",
                Description = user.ShopDescription ?? "",
                AvatarPath = user.Image ?? "",
                AverageRating = averageRating,
                TotalReview = feedbackCount
            };
            return new ResponseModel { Data = result};

        }
    }
}
