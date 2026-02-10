using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be longer than 6 symbols")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Telephone { get; set; }

        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }

}
