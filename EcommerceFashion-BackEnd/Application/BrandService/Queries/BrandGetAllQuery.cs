using Application.BrandService.Models;
using Application.BrandService.Models.ResponseModels;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BrandService.Queries
{
    public record BrandGetAllQuery : IRequest<ResponseModel>;
    public class BrandGetAllQueryHandler : IRequestHandler<BrandGetAllQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandGetAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(BrandGetAllQuery request, CancellationToken cancellationToken)
        {
            var brandRepository = await _unitOfWork.BrandRepository.GetAllAsync(
                filter: _ => _.IsDeleted == false,
                include: brandRepository => brandRepository.Include(_ => _.BrandProductCategoryDatas)
                ); 
           
            var brandListModels = brandRepository.Data.Select(_ => new BrandModel
            {
                Id = _.Id,
                Name = _.Name,
                VitualPath = _.VitualPath,
            });
            return new ResponseModel { Data = brandListModels};
        }
    }
}
