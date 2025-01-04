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
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }

      

        public async Task<bool> HasFeedbacked(Guid? shopId, Guid userId)
        {
            if(shopId == null)
            {
                return await _dbSet.AnyAsync(_ => _.CreatedById == userId && _.FeedbackType == Domain.Enums.FeedbackType.Website);
            }
            return await _dbSet.AnyAsync(_ => _.CreatedById == userId && _.ShopId == shopId && _.FeedbackType == Domain.Enums.FeedbackType.Shop);
        }
    }
}
