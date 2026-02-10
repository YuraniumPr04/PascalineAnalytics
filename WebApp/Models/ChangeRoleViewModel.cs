using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class ChangeRoleViewModel
    {
        public string UserID {  get; set; }

        public string Email { get; set; }

        public List<IdentityRole> AllRoles { get; set; }

        public List<string> UserRoles { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
