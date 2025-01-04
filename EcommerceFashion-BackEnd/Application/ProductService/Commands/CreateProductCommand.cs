/*using Application.ProductService.Queries;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductService.Commands
{
    public record CreateProductCommand : IRequest<Result<CreateProductDto>>;
    public record CreateProductDto
    {
        public Guid Id { get; set; }
        public string AvatarPath { get; set; }
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailDescription { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Status { get; set; }
        public string? SKU { get; set; }
        public List<IFormFile> productImages { get; set; }
    }
    public class CreateProductQueryHandler : IRequestHandler<CreateProductCommand, Result<CreateProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepository;
        public CreateProductQueryHandler(IUnitOfWork unitOfWork, IRepository<Product> productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<Result<List<AllProductDto>>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
           
            
        }

    }

}
*/