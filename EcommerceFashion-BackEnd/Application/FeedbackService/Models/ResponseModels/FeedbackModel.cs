using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedbackService.Models.ResponseModels
{
    public class FeedbackModel : BaseEntity
    {
        public Guid? ProductId { get; set; } = null;
        public Guid? ShopId { get; set; } = null;
        public string AuthorName { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ICollection<string>? Images { get; set; } = new List<string>();
    }
}
