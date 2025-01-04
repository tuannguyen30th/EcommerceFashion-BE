using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedbackService.Models.RequestModels
{
    public class FeedbackAddForShopOrWebsiteModel
    {
        public Guid? ShopId { get; set; }
        public required int Rating { get; set; }
        public string Description { get; set; }
        public FeedbackType FeedbackType { get; set; }
    }
}
