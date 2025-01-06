using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IProductVariantRepository : IGenericRepository<ProductVariant>
    {
        Task<int> Stock(Guid id);
    }
}
