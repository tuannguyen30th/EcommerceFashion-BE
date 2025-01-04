using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Models.RequestModels
{
    public class EmailModel
    {
        [Required, EmailAddress]
        [StringLength(256)]
        public required string Email { get; set; }
    }
}
