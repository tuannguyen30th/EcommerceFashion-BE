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
        public Guid AccountId { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; } = null!;
        [ForeignKey(nameof(RoleId))]

        public Role Role { get; set; } = null!;

    }
}
