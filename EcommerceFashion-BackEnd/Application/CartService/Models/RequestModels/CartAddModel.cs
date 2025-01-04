using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.CartService.Models.RequestModels
{
    public class CartAddModel
    {
      
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
        public int Quantity { get; set; }

        [MinLength(1, ErrorMessage = "At least one attribute is required.")]
        public List<Guid> AttributeIds { get; set; } = new List<Guid>();
    }
}
