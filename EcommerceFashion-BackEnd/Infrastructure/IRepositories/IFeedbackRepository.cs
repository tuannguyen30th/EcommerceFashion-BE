using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<bool> HasFeedbacked(Guid? shopId, Guid userId);
    }
}
