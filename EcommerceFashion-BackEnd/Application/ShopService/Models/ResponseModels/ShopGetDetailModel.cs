using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ShopService.Models.ResponseModels
{
    public class ShopGetDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AvatarPath { get; set; }
        public float AverageRating { get; set; }
        public int TotalReview { get; set; }
    }
}
