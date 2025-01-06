using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
        
        [ForeignKey(nameof(CreatedById))]
        public Account Account { get; set; }
        public Guid? ShopId { get; set; }
        [ForeignKey(nameof(ShopId))]
        public Account? Shop { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public required int Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public FeedbackType FeedbackType { get; set; } 
        public ICollection<Image>? Images { get; set; } = new List<Image>();
    }

}
