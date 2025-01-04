using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WishListAttributeRepository : GenericRepository<WishListAttribute>, IWishListAttributeRepository
    {
        public WishListAttributeRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }

        public async Task<List<WishListAttribute>> GetByWishList(Guid id)
        {
            return await _dbSet.Where(_ => _.WishListId == id).ToListAsync();
        }
    }
}
