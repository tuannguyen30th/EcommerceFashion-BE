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
    [Table("ProductVideos")]
    public class ProductVideo : BaseEntity
    {
        public required Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public required string VideoPath { get; set; }
    }
}
