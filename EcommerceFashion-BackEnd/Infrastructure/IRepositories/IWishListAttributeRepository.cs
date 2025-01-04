using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IWishListAttributeRepository : IGenericRepository<WishListAttribute>
    {
        Task<List<WishListAttribute>> GetByWishList(Guid id);
    }
}
