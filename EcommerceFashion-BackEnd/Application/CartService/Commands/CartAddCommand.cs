using Application.CartService.Models;
using Application.CartService.Models.RequestModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using System.Linq;

namespace Application.CartService.Commands
{
    public record CartAddCommand(CartAddModel cartAddModel) : IRequest<ResponseModel>;

    public class CartAddCommandHandler : IRequestHandler<CartAddCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public CartAddCommandHandler(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public async Task<ResponseModel> Handle(CartAddCommand request, CancellationToken cancellationToken)
        {
            var model = request.cartAddModel;

            var currentUserId = _claimService.GetCurrentUserId;
            if (!currentUserId.HasValue)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized."
                };
            }

            var product = await _unitOfWork.ProductRepository.GetAsync(model.ProductId);
            if (product == null) return new ResponseModel { Message = "Product not found.", Code = StatusCodes.Status404NotFound };


            var productVariantRepo = await _unitOfWork.ProductVariantAttributeValueDataRepository.CheckStock(model.ProductId, model.AttributeIds, _ => _.Include(_ => _.ProductVariant) .Include(_ => _.ProductAttributeValue));
            if (productVariantRepo == null) return new ResponseModel { Message = "Invalid attributes.", Code = StatusCodes.Status400BadRequest };

            var totalStock = productVariantRepo.ProductVariant.Stock;
            if (totalStock < model.Quantity) return new ResponseModel { Message = "Out of stock.", Code = StatusCodes.Status400BadRequest };
            if (totalStock <= 0) return new ResponseModel { Message = "Out of stock.", Code = StatusCodes.Status400BadRequest };


            var existingCart = await _unitOfWork.CartRepository.ExistingCart(currentUserId.Value, model.ProductId, model.AttributeIds);

            if (existingCart != null)
            {
                existingCart.Quantity += model.Quantity;
                _unitOfWork.CartRepository.Update(existingCart);
            }
            else
            {
                var newCart = new Cart
                {
                    CreatedById = currentUserId.Value,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    CartItems = model.AttributeIds.Select(attrId => new CartItem
                    {
                        Id = Guid.NewGuid(),
                        ProductVariantAttributeValueDataId = attrId
                    }).ToList()
                };

                await _unitOfWork.CartRepository.AddAsync(newCart);
            }

            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel { Message = "Product added to cart successfully." };
        }

    }
}
