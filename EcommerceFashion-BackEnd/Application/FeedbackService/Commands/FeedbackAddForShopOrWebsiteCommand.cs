using Application.FeedbackService.Models.RequestModels;
using Azure.Core;
using Domain.Common;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedbackService.Commands
{
    public record FeedbackAddForShopOrWebsiteCommand(FeedbackAddForShopOrWebsiteModel createFeedbackForShop) : IRequest<ResponseModel>;
    public class FeedbackAddForShopOrWebsiteCommandHandler : IRequestHandler<FeedbackAddForShopOrWebsiteCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public FeedbackAddForShopOrWebsiteCommandHandler(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public async Task<ResponseModel> Handle(FeedbackAddForShopOrWebsiteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _claimService.GetCurrentUserId;
            if (!currentUserId.HasValue)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized."
                };
            }
            var feedbackRequest = request.createFeedbackForShop;

            if (feedbackRequest.Rating <= 0 || string.IsNullOrWhiteSpace(feedbackRequest.Description) )
            {
                return new ResponseModel { Code = StatusCodes.Status500InternalServerError, Message = "Invalid input. All fields must be filled." };
            }

            if (feedbackRequest.FeedbackType == Domain.Enums.FeedbackType.Shop)
            {
                if (feedbackRequest.ShopId == null)
                {
                    return new ResponseModel { Data = null, Code = StatusCodes.Status500InternalServerError, Message = "ShopId is required for shop feedback." };
                }

                var hasFeedbacked = await _unitOfWork.FeedbackRepository.HasFeedbacked(feedbackRequest.ShopId ,currentUserId.Value);
                if (hasFeedbacked)
                {
                    return new ResponseModel { Code = StatusCodes.Status400BadRequest, Message = "You have already rated this shop." };
                }
            }
            else if (feedbackRequest.FeedbackType == Domain.Enums.FeedbackType.Website)
            {
                var hasFeedbacked = await _unitOfWork.FeedbackRepository.HasFeedbacked(null ,currentUserId.Value);
                if (hasFeedbacked)
                {
                    return new ResponseModel { Code = StatusCodes.Status400BadRequest, Message = "You have already rated this website." };
                }
            }
            else
            {
                return new ResponseModel { Code = StatusCodes.Status400BadRequest, Message = "Invalid feedback type." };
            }

            var newFeedback = new Feedback
            {
                CreatedById = currentUserId.Value,
                ShopId = feedbackRequest.FeedbackType == Domain.Enums.FeedbackType.Shop ? feedbackRequest.ShopId : null,
                Rating = feedbackRequest.Rating,
                Description = feedbackRequest.Description,
                FeedbackType = feedbackRequest.FeedbackType
            };

            await _unitOfWork.FeedbackRepository.AddAsync(newFeedback);

            var result = await _unitOfWork.SaveChangeAsync();

            return result > 0
                ? new ResponseModel { Data = newFeedback, Message = "Feedback successfully created." }
                : new ResponseModel { Code = StatusCodes.Status400BadRequest, Message = "Failed to create feedback." };
        }
    }
}
