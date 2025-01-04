/*using Application.ProductService.Models;
using Application.ProductService.Models.RequestModels;
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

namespace Application.ProductService.Queries
{
    public record GetAllProductsQuery(ProductFilterModel productFilterModel) : IRequest<ResponseModel>;
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductFilterModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {

            var productLists = await _unitOfWork.ProductRepository.GetAllAsync(
                pageIndex: request.productFilterModel.PageIndex,
                pageSize: request.productFilterModel.PageSize,
                filter: _ => (request.productFilterModel.MinPrice == null || _.SalePrice >= request.productFilterModel.MinPrice) &&
                (request.productFilterModel.MaxPrice == null || _.SalePrice <= request.productFilterModel.MaxPrice)
                && (string.IsNullOrEmpty(request.productFilterModel.Search) ||
                     _.Name.ToLower().Contains(request.productFilterModel.Search.ToLower())),
                orderBy: _ =>
                {
                    switch (request.productFilterModel.Order.ToLower())
                    {
                        case "SalePrice":
                            return request.productFilterModel.OrderByDescending ?
                            _.OrderByDescending(_ => _.SalePrice) :
                            _.OrderBy(_ => _.SalePrice);
                        case "DefaultPrice":
                            return request.productFilterModel.OrderByDescending ?
                            _.OrderByDescending(_ => _.DefaultPrice) :
                            _.OrderBy(_ => _.DefaultPrice);
                        default:
                            return request.productFilterModel.OrderByDescending ?
                            _.OrderByDescending(_ => _.CreationDate) :
                            _.OrderBy(_ => _.CreationDate);
                    }
                },
                include: "ProductAttributeDatas, ProductCategoryDatas, Images, ProductVideos, Feedbacks"
            );

            if (productList.Data.Any())
            {
                var productModelList = productList.Data.Select(p => new GetAllProductsModel
                {
                    Id = p.Id,
                    AvatarPath = p.AvatarPath,
                    Name = p.Name,
                    ShortDescription = p.ShortDescription,
                    DetailDescription = p.DetailDescription,
                    DefaultPrice = p.DefaultPrice,
                    SalePrice = p.SalePrice,
                    Stock = p.Stock,
                    Weight = (float)p.Weight,
                    Length = (float)p.Length,
                    Width = (float)p.Width,
                    Height = (float)p.Height,
                    Status = p.Status,
                    SKU = p.SKU
                }).ToList();

                return new Pagination<GetAllProductsModel>(
                    productModelList,
                    request.productFilterModel.PageIndex,
                    request.productFilterModel.PageSize,
                    productList.TotalCount
                );
            }

            return new Pagination<GetAllProductsModel>(new List<GetAllProductsModel>(), request.productFilterModel.PageIndex, request.productFilterModel.PageSize, 0);
        }

    }
}
*/