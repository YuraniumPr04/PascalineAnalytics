using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Account
{
    public class IndexViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        public string Telephone { get; set; }
    }
}
