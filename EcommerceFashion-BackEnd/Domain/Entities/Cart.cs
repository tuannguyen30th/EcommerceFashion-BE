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
    [Table("Carts")]
    public class Cart : BaseEntity
    {
        public required Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [ForeignKey(nameof(CreatedById))]
        public Account Account { get; set; } = null!;
    }

}
