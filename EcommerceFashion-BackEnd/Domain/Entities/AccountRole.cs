using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("AccountRoles")]
    public class AccountRole : BaseEntity
    {
        // Foreign key
        public Guid AccountId { get; set; }
        public Guid RoleId { get; set; }

        // Relationship
        public Account Account { get; set; } = null!;
        public Role Role { get; set; } = null!;

    }
}
