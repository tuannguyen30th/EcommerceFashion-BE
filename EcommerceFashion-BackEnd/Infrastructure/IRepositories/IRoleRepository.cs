using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> FindByNameAsync(string name);
        Task<List<Role>> GetAllByAccountIdAsync(Guid accountId);
    }
}
