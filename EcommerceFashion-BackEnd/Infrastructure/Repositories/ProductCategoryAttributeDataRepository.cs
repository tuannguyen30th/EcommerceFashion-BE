using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductCategoryAttributeDataRepository : GenericRepository<ProductCategoryAttributeData>, IProductCategoryAttributeDataRepository
    {
        public ProductCategoryAttributeDataRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }
    }
}
