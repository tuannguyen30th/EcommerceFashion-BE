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
    public class WishListRepository : GenericRepository<WishList>, IWishListRepository
    {
        private readonly EcommerceFashionDbContext _context;
        public WishListRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
            _context = context;
        }


        public async Task<WishList> ExistingWishList(Guid accountId, Guid productId, List<Guid> attributeIds)
        {
            return await _context.WishLists
                         .Where(_ =>
                                _.CreatedById == accountId &&
                                _.ProductId == productId &&
                                attributeIds.All(attrId => _.WishListAttributes
                                 .Any(wishListAttr => wishListAttr.ProductVariantAttributeValueDataId == attrId)))
                         .Include(_ => _.WishListAttributes)
                         .FirstOrDefaultAsync();
        }

        public async Task<List<WishList>> GetAll(Guid accountId)
        {
            return await _context.WishLists
                .Where(_ => _.CreatedById == accountId)
                .Include(_ => _.Product)
                .Include(_ => _.WishListAttributes)
                .Include("WishListAttributes.ProductVariantAttributeValueData.ProductAttributeValue")
                .ToListAsync();
        }

       
    }
}
