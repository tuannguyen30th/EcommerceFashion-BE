using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> ExistingCart(Guid accountId, Guid productId, List<Guid> attributeIds);
    }
}
