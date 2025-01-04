using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Feedbacks")]
    public class Feedback : BaseEntity
    {
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Account Account { get; set; } = null!;
        public Guid? ShopId { get; set; }
        public Account? Shop { get; set; } = null!;
        public required int Rating { get; set; }
        public string Description { get; set; }
        public FeedbackType FeedbackType { get; set; }
        public ICollection<Image>? Images { get; set; } = new List<Image>();

    }
}
