using WebApp.Areas.Admin.Models.SubscriptionPlan;

namespace WebApp.Areas.Admin.Models
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public SubscriptionPlanViewModel ActiveSubscriptionPlan { get; set; }
        public long StorageUsed { get; set; }
        public int RequestsUsed { get; set; }
    }
}
