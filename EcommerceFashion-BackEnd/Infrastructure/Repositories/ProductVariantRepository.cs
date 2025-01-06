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
    public class ProductVariantRepository : GenericRepository<ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }

        public async Task<int> Stock(Guid id)
        {
            var stock = _dbSet.Where(_ => _.ProductId == id).Sum(_ => _.Stock);
            return stock;
        }
    }
}
