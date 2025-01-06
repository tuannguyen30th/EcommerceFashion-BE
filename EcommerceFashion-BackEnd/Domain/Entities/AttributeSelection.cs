using Domain.Common;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("AttributeSelections")]
    public class AttributeSelection : BaseEntity
    {
        public required Guid AttributeId { get; set; }
        [ForeignKey(nameof(AttributeId))]
        public ProductAttribute Attribute { get; set; } = null!;
        public required string SelectionValue { get; set; }
        public int Order {  get; set; }

    }
}
