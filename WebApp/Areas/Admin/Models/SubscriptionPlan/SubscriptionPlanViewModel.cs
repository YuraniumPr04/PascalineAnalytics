namespace WebApp.Areas.Admin.Models.SubscriptionPlan
{
    public class SubscriptionPlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public string Description { get; set; }
        public int StorageLimitGb { get; set; }      
        public int DailyRequestsLimit { get; set; }
        public decimal Price { get; set; }
    }
}
