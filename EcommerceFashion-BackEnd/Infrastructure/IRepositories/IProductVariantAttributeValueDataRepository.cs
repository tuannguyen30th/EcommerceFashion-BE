using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IProductVariantAttributeValueDataRepository : IGenericRepository<ProductVariantAttributeValueData>
    {
        Task<ProductVariantAttributeValueData> CheckStock(Guid productId, List<Guid> attributeIds, Func<IQueryable<ProductVariantAttributeValueData>, IQueryable<ProductVariantAttributeValueData>>? include = null);
    }
}
