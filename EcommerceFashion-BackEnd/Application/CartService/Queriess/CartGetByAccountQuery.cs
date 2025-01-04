using Application.CartService.Models;
using Application.CartService.Models.ResponseModels;
using Application.WishListService.Models;
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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartService.Queriess
{
    public record CartGetByAccountQuery(Guid accountId) : IRequest<ResponseModel>;
    public class CartGetByAccountQueryHandler : IRequestHandler<CartGetByAccountQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public CartGetByAccountQueryHandler(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public async Task<ResponseModel> Handle(CartGetByAccountQuery request, CancellationToken cancellationToken)
        {
            var idRequest = request.accountId;

            var currentUserId = _claimService.GetCurrentUserId;
            if (!currentUserId.HasValue)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized."
                };
            }

          
            var cartLists = await _unitOfWork.CartRepository.GetAllAsync(
                                                            filter: cart => cart.CreatedById == currentUserId,
                                                            include: query => query.Include("CartItems.ProductVariantAttributeValue.ProductAttributeValue")
                                                            .Include("Product")
                                                            );

            if (!cartLists.Data.Any())
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "No cart items found for the account."
                };
            }

            var cartModels = cartLists.Data.Select(cart => new CartModel
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                Name = cart.Product?.Name ?? "Unknown Product",
                AvatarPath = cart.Product?.AvatarPath ?? string.Empty,
                DefaultPrice = cart.Product?.DefaultPrice ?? 0,
                SalePrice = Math.Max(cart.Product?.SalePrice ?? 0, 0),
                Quantity = cart.Quantity,
                Discount = (cart.Product?.DefaultPrice > 0 && cart.Product?.SalePrice > 0)
                    ? (float)((cart.Product.DefaultPrice - cart.Product.SalePrice) / cart.Product.DefaultPrice) * 100
                    : 0,
                ProductAttributeLists = cart?.CartItems?.Select(item => new ProductAttributesModel
                {
                    CartItemId = item.Id,
                    Id = item.ProductVariantAttributeValueData.ProductAttributeValue.Id,
                    Name = item.ProductVariantAttributeValueData?.ProductAttributeValue?.Value
                }).ToList()
            }).ToList();

            return new ResponseModel
            {
                Data = cartModels,
                Message = "Cart retrieved successfully."
            };
        }

    }
}
