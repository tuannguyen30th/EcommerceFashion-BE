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
    [Table("Images")]
    public class Image : BaseEntity
    {
        public Guid? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public Guid? FeedbackId { get; set; }
        [ForeignKey(nameof(FeedbackId))]
        public Feedback? Feedback { get; set; }
        public required string ImagePath { get; set; } 
        public required string ImageAlt { get; set; }  
        //public required int Dimension { get; set; }   
    }
}
