using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> FindByEmailAsync(string email, Func<IQueryable<Account>, IQueryable<Account>>? include = null);
        Task<Account?> FindByUsernameAsync(string username, Func<IQueryable<Account>, IQueryable<Account>>? include = null);
        Task<List<Guid>> GetValidAccountIdsAsync(List<Guid> accountIds);
        Task<List<Account>> ShopGetAll();
        Task<string> GetAuthorNameById(Guid? createdById);
        //Task<Role> GetByAccountIdAsync(Guid accountId);
    }
}
