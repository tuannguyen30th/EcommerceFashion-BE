using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Credits")]
    public class Credit : BaseEntity
    {
        public decimal Balance { get; set; }
        public ICollection<CreditHistory> CreditHistories { get; set; } = new List<CreditHistory>();
    }
}
