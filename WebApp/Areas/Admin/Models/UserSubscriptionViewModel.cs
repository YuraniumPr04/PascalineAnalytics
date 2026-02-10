using WebApp.Areas.Admin.Models.SubscriptionPlan;

namespace WebApp.Areas.Admin.Models
{
    public class UserSubscriptionViewModel
    {
        public SubscriptionPlanViewModel SubscriptionPlan { get; set; }
        public DateTime ActivationTime { get; set; }
        public DateTime ExperationTime { get; set; }
    }
}
