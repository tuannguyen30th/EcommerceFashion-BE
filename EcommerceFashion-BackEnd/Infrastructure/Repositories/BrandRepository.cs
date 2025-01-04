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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }

        public async Task<Brand> FindByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(_ => _.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
        }
    }
}
