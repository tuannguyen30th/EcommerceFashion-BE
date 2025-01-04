using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Models.RequestModels
{
    public class AccountSignInModel
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(128, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}
