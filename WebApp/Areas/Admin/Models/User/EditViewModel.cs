using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.Models.User
{
    public class EditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        public string Telephone { get; set; }

        [Required(ErrorMessage = "Choose role")]
        public string SelectedRole { get; set; }

        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}