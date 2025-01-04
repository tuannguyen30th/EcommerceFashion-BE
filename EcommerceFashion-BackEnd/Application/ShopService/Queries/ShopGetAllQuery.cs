using Application.ShopService.Models;
using Application.ShopService.Models.ResponseModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ShopService.Queries
{
    public record ShopGetAllQuery : IRequest<ResponseModel>;
    public class ShopGetAllQueryHandler : IRequestHandler<ShopGetAllQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopGetAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(ShopGetAllQuery request, CancellationToken cancellationToken)
        {
            var shopLists = await _unitOfWork.AccountRepository.ShopGetAll();
            var shopModels = shopLists.Select(_ => new ShopModel
            {
                Id = _.Id,
                Name = _.ShopName ?? "",
                Description = _.ShopDescription ?? ""
            });
            return new ResponseModel { Data = shopModels};
        }
    }
}
