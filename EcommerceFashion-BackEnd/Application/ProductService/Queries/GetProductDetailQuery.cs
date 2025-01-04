/*using Domain.Common;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.ProductService.Queries
{
    public record GetProductDetailQuery(Guid Id) : IRequest<Result<ProductDto>>;
    public record ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, Result<ProductDto>>
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IRepository<Product> _productRepository;
        public GetProductDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = unitOfWork.GetRepository<Product>();
        }
        public async Task<Result<ProductDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            //validation
            if (request is null)
            {
                return Error<ProductDto>.Validation("Validation Error", "...");
            }

            //query product
            var product = await _productRepository.GetAsync(x => x.Id == request.Id);
            if (product is null)
            {
                return Error<ProductDto>.NotFound("Product Not Found", "...");
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name
            };
            return Result<ProductDto>.Success(productDto, "Get product successfully");
        }
    }
}
*/