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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }
       
        public async Task<Cart> ExistingCart(Guid accountId, Guid productId, List<Guid> attributeIds)
        {
            return await _dbSet
                            .Where(cart => cart.CreatedById == accountId && cart.ProductId == productId)
                            .Include(cart => cart.CartItems)
                            .FirstOrDefaultAsync(cart => cart.CartItems
                                .Any(cartItem => attributeIds.All(attrId => attrId == cartItem.ProductVariantAttributeValueDataId)));
        }
    }
}
