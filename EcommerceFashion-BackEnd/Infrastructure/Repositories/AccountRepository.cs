using Domain.Entities;
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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(EcommerceFashionDbContext context, IClaimService claimService) : base(context, claimService)
        {
        }

        public async Task<Account?> FindByEmailAsync(string email,
            Func<IQueryable<Account>, IQueryable<Account>>? include = null)
        {
            IQueryable<Account> query = _dbSet;
            if (include != null) query = include(query);

            return await query.FirstOrDefaultAsync(account => account.Email == email);
        }

        public async Task<Account?> FindByUsernameAsync(string username,
            Func<IQueryable<Account>, IQueryable<Account>>? include = null)
        {
            IQueryable<Account> query = _dbSet;
            if (include != null) query = include(query);

            return await query.FirstOrDefaultAsync(account => account.Username == username);
        }

        public async Task<string> GetAuthorNameById(Guid? createdById)
        {
            if (createdById == null)
                return "Unknown";

            var account = await _dbSet.FindAsync(createdById.Value);
            if (account == null)
                return "Unknown";

            return account.FirstName + " " + account.LastName;
        }

        //public async Task<Role> GetByAccountIdAsync(Guid accountId)
        //{
        //    var select =  await _dbSet.FirstOrDefaultAsync(_ => _.Id == accountId);
        //    var result = select.Role;
        //    return result;
        //}

        public async Task<List<Guid>> GetValidAccountIdsAsync(List<Guid> accountIds)
        {
            return await _dbSet.Where(account => accountIds.Contains(account.Id) && !account.IsDeleted)
                .Select(account => account.Id).Distinct().ToListAsync();
        }

        public async Task<List<Account>> ShopGetAll()
        {
            return await _dbSet.Where(_ => _.IsShop == true && _.IsDeleted == false).ToListAsync();
        }
    }
}
