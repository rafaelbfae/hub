using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Level = 0;
            IsUser = true;
        }

        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Confirm email")]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Is administrator")]
        public bool IsAdministrator { get; set; }

        [Display(Name = "Is manager")]
        public bool IsManager { get; set; }

        [Display(Name = "Is User")]
        public bool IsUser { get; set; }

        [Required]
        [Display(Name = "Level")]
        public int Level { get; set; }
    }
}
