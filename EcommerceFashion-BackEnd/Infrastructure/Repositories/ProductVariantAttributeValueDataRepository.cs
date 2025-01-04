using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductVariantAttributeValueDataRepository : GenericRepository<ProductVariantAttributeValueData>, IProductVariantAttributeValueDataRepository
    {
        public ProductVariantAttributeValueDataRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }
        public async Task<ProductVariantAttributeValueData> CheckStock(Guid productId, List<Guid> attributeIds, Func<IQueryable<ProductVariantAttributeValueData>, IQueryable<ProductVariantAttributeValueData>>? include = null)
        {
            IQueryable<ProductVariantAttributeValueData> query = _dbSet;
            if (include != null) query = include(query);
            var result = await query.FirstOrDefaultAsync(_ => _.ProductVariant.ProductId == productId && attributeIds.Contains(_.ProductAttributeValueId));

            return result;


        }
    }
}
