using Domain.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Accounts")]
    public class Account : BaseEntity
    {

        // Required information
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;

        // Personal information
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }

        // Status
        public bool EmailConfirmed { get; set; } = false;
        public bool PhoneNumberConfirmed { get; set; } = false;

        // System
        public string? VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiryTime { get; set; }
        public string? ResetPasswordToken { get; set; }
        public string? ShopName { get; set; }
        public string? ShopDescription { get; set; }
        public bool? IsShop { get; set; } = false;
        public Guid? CreaditId { get; set; }
        [ForeignKey(nameof(CreaditId))]
        public Credit Credit { get; set; } = null!;
        public virtual ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();
        public ICollection<Feedback> FeedBacksAsReviewer { get; set; } = new List<Feedback>();
        public ICollection<Feedback> FeedBacksAsShop { get; set; } = new List<Feedback>();
        public ICollection<Product>? Products { get; set; } = new List<Product>();
        public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
