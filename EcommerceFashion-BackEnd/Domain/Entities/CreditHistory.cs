using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("CreditHistories")]
    public class CreditHistory : BaseEntity
    {
        public required Guid CreditId { get; set; }

        [ForeignKey(nameof(CreditId))]
        public Credit Credit { get; set; } = null!;
        public required decimal Amount { get; set; }
    }

}
