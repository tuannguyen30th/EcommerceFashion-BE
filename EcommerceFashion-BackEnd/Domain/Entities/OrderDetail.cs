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
    [Table("OrderDetails")]
    public class OrderDetail : BaseEntity
    {
        public required Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public required Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public ICollection<OrderDetailAttribute> OrderDetailAttributes { get; set; } = new List<OrderDetailAttribute>();

    }
}
