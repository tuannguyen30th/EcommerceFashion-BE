using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IWishListRepository : IGenericRepository<WishList>
    {
        Task<WishList> ExistingWishList(Guid accountId, Guid productId, List<Guid> attributeIds);
        Task<List<WishList>> GetAll(Guid accountId);
    }
}
