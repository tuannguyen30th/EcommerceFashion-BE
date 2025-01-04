using Application.ProductCategoryService.Models;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductCategoryService.Queries
{
    public record ProductCategoryGetQuery : IRequest<ResponseModel>;
    public class ProductCategoryGetQueryHandler : IRequestHandler<ProductCategoryGetQuery, ResponseModel>
    {
        public readonly IUnitOfWork _unitOfWork;

        public ProductCategoryGetQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(ProductCategoryGetQuery request, CancellationToken cancellationToken)
        {
            var productCategoryLists = await _unitOfWork.ProductCategoryRepository.GetAllAsync(
                filter: _ => (_.ParentId == null && _.IsDeleted == false)
                );
            var productCategoryModels = productCategoryLists?.Data?.Select(_ => new ProductCategoryModel
            {
                Id = _.Id,
                Name = _.Name,
                VirtualPath = _.VirtualPath,
            });
            return new ResponseModel { Data = productCategoryModels};
        }
    }
}
