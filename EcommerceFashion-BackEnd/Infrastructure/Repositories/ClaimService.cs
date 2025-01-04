using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClaimService : IClaimService
    {
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var accountIdClaim = identity?.FindFirst("accountId");
            if (accountIdClaim != null && Guid.TryParse(accountIdClaim.Value, out var currentUserId))
                GetCurrentUserId = currentUserId;
        }

        public Guid? GetCurrentUserId { get; }
    }
}
