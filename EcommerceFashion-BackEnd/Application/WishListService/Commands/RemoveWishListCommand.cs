/*using Domain.Common;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WishListService.Commands
{
    public record RemoveWishListCommand(Guid wishListId) : IRequest<ResponseModel>;
    public class RemoveWishListCommandHandler : IRequestHandler<RemoveWishListCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveWishListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(RemoveWishListCommand request, CancellationToken cancellationToken)
        {
            var wishListAttributeRepository = _unitOfWork.GetRepository<WishListAttribute>();
            var wishListRepository = _unitOfWork.GetRepository<WishList>();

            var wishListAttribute = await wishListAttributeRepository.GetAsync(request.wishListId, "WishList");

            if (wishListAttribute == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Wish list not found."
                };
            }

            wishListAttributeRepository.HardDelete(wishListAttribute);
            var wishList = wishListAttribute.WishList;
            wishListRepository.HardDelete(wishList);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel
            {
                Status = true,
                Message = "Wish list removed successfully."
            };
        }
    }
}
*/