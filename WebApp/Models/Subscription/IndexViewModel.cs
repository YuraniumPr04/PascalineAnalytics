using WebApp.Areas.Admin.Models.SubscriptionPlan;

namespace WebApp.Models.Subscription
{
    public class IndexViewModel
    {
        public SubscriptionPlanViewModel CurrentSubscriptionPlan { get; set; }
        public List<SubscriptionPlanViewModel> SubscriptionPlans { get; set; } = new List<SubscriptionPlanViewModel>();
    }
}
