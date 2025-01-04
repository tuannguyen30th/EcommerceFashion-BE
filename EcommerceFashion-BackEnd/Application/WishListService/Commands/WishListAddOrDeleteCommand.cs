using Application.WishListService.Models;
using Application.WishListService.Models.RequestModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace Application.WishListService.Commands
{
    public record WishListAddOrDeleteCommand(WishListAddModel wishListAddModel) : IRequest<ResponseModel>;

    public class WishListAddOrDeleteCommandHandler : IRequestHandler<WishListAddOrDeleteCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public WishListAddOrDeleteCommandHandler(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public async Task<ResponseModel> Handle(WishListAddOrDeleteCommand request, CancellationToken cancellationToken)
        {
            int result;
            var currentUserId = _claimService.GetCurrentUserId;
            if (!currentUserId.HasValue)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized."
                };
            }
            var model = request.wishListAddModel;

            var product = await _unitOfWork.ProductRepository.GetAsync(model.ProductId);
            if (product == null) return new ResponseModel { Message = "Product not found.", Code = StatusCodes.Status404NotFound };

            var existingWishList = await _unitOfWork.WishListRepository.ExistingWishList(currentUserId.Value, model.ProductId, model.AttributeIds);

            if (existingWishList != null)
            {
                var findWishListExists = await _unitOfWork.WishListAttributeRepository.GetByWishList(existingWishList.Id);
                _unitOfWork.WishListAttributeRepository.HardRemoveRange(findWishListExists);
                _unitOfWork.WishListRepository.HardRemove(existingWishList);
                result = await _unitOfWork.SaveChangeAsync();
                return result > 0 ? new ResponseModel { Message = "Product remove from wishList successfully." } : new ResponseModel { Message = "Product remove from wishList unsuccessfully." };
            }

            var newCart = new WishList
            {
                CreatedById = currentUserId.Value,
                ProductId = model.ProductId,
                WishListAttributes = model.AttributeIds.Select(attrId => new WishListAttribute
                {
                    Id = Guid.NewGuid(),
                    ProductVariantAttributeValueDataId = attrId
                }).ToList()
            };

            await _unitOfWork.WishListRepository.AddAsync(newCart);

            result = await _unitOfWork.SaveChangeAsync();

            return result > 0 ? new ResponseModel { Message = "Product add to wishList successfully." } : new ResponseModel { Message = "Product add to wishList unsuccessfully." };
        }
    }
}
