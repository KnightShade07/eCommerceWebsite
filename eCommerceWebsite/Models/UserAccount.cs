using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceWebsite.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(200)]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email: ")]
        public string ConfirmEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(120, MinimumLength = 8, ErrorMessage ="Password must be between {2} and {1} characters long")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password: ")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Date)] //Time is ignored.
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Username Or Email: ")]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
