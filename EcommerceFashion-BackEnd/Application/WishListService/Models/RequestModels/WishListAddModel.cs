using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WishListService.Models.RequestModels
{
    public class WishListAddModel
    {
        
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public List<Guid> AttributeIds { get; set; }
    }
}
