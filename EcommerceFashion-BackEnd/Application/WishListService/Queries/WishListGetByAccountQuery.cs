using Application.WishListService.Models;
using Domain.Common;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;

namespace Application.WishListService.Queries
{
    public record WishListGetByAccountQuery() : IRequest<ResponseModel>;

    public class WishListGetByAccountQueryHandler : IRequestHandler<WishListGetByAccountQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public WishListGetByAccountQueryHandler(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public async Task<ResponseModel> Handle(WishListGetByAccountQuery request, CancellationToken cancellationToken)
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
            var findWishList = await _unitOfWork.WishListRepository.GetAll(currentUserId.Value);


            if (!findWishList.Any())
            {
                return new ResponseModel
                {
                    Data = null,
                    Code = StatusCodes.Status404NotFound,
                    Message = "No wishlist items found for the account."
                };
            }

            var wishListModel = findWishList.Select(_ => new GetAllWishListsByAccountModel
            {
                Id = _.Id,
                Name = _.Product.Name,
                AvatarPath = _.Product.AvatarPath,
                DefaultPrice = _.Product.DefaultPrice,
                SalePrice = Math.Max(_.Product.SalePrice, 0),
                Discount = _.Product.SalePrice > 0 && _.Product.DefaultPrice > 0
                    ? (float)((_.Product.DefaultPrice - _.Product.SalePrice) / _.Product.DefaultPrice) * 100
                    : 0,
                ProductAttributeLists = _.WishListAttributes.Select(_ => new ProductAttributeListModel
                {
                    Id = _.ProductVariantAttributeValueData.ProductAttributeValue.Id,
                    WishListAttributeId = _.Id,
                    Name = _.ProductVariantAttributeValueData.ProductAttributeValue.Value
                }).ToList()
            });

            return new ResponseModel
            {
                Data = wishListModel,
                Message = "Wishlist retrieved successfully."
            };
        }
    }
}
