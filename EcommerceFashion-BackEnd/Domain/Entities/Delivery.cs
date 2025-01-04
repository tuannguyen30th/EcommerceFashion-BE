using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Deliveries")]
    public class Delivery : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
