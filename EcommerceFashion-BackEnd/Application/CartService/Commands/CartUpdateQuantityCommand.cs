using Domain.Common;
using Domain.Entities;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartService.Commands
{
    public record CartUpdateQuantityCommand(Guid cartId, int quantity) : IRequest<ResponseModel>;
    public class CartUpdateQuantityCommandHandler : IRequestHandler<CartUpdateQuantityCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartUpdateQuantityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(CartUpdateQuantityCommand request, CancellationToken cancellationToken)
        {

            var cart = await _unitOfWork.CartRepository.GetAsync(request.cartId, _ => _.Include(_ => _.Product));
            if (request.quantity <= 0)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Quantity must be greater than 0."
                };
            }
            if (cart == null)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Cart item not found."
                };
            }         
            var product = cart.ProductId;
            var productAttributes = cart.CartItems.Select(_ => _.ProductVariantAttributeValueDataId).ToList();
            var productVariantRepo = await _unitOfWork.ProductVariantAttributeValueDataRepository.CheckStock(cart.ProductId, productAttributes, _ => _.Include(_ => _.ProductVariant).Include(_ => _.ProductAttributeValue));
            if (productVariantRepo == null) return new ResponseModel { Message = "Invalid attributes.", Code = StatusCodes.Status400BadRequest };

            var totalStock = productVariantRepo.ProductVariant.Stock;
            if (totalStock < request.quantity) return new ResponseModel { Message = "Out of stock.", Code = StatusCodes.Status400BadRequest };
            if (totalStock <= 0) return new ResponseModel { Message = "Out of stock.", Code = StatusCodes.Status400BadRequest };
         
            cart.Quantity = request.quantity;
            _unitOfWork.CartRepository.Update(cart);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel
            {
                Message = "Cart item quantity adjusted successfully."
            };
        }
    }
}
