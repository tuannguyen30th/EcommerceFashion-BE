﻿using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }

        public async Task<Role?> FindByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(role => role.Name == name);
        }



        public async Task<List<Role>> GetAllByAccountIdAsync(Guid accountId)
        {
            return await _dbSet.Where(role => role.AccountRoles.Any(accountRole => accountRole.AccountId == accountId))
            .ToListAsync();
        }
    }
}
