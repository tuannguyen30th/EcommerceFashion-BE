﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("OrderDetailAttributes")]
    public class OrderDetailAttribute : BaseEntity
    {
        public required Guid OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; } = null!;
        public required Guid ProductVariantAttributeValueDataId { get; set; }
        public ProductVariantAttributeValueData ProductVariantAttributeValueData { get; set; } = null!;

    }
}
