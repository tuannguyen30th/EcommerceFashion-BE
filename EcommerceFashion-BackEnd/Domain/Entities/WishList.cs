using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("WishLists")]
    public class WishList : BaseEntity
    {
        [ForeignKey(nameof(CreatedById))]
        public Account Account { get; set; } = null!;
        public required Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public ICollection<WishListAttribute> WishListAttributes { get; set; } = new List<WishListAttribute>();
    }

}
