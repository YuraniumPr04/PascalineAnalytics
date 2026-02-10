using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.Models.SubscriptionPlan
{
    public class EditViewModel
    {   
        public int Id { get; set; }

        [Display(Name = "Plan name", Prompt = "Example: Premium")]
        [Required(ErrorMessage = "Please, enter subscription plan name")]
        [StringLength(100, ErrorMessage = "Name can not be longer than 100 symbols")]
        public string Name { get; set; }

        [Display(Name = "Duration (days)", Prompt = "30")]
        [Required(ErrorMessage = "Enter duration")]
        [Range(1, 3650, ErrorMessage = "Duration must be in range from 1 to 3650 days")]
        public int? DurationDays { get; set; }

        [Display(Name = "Description", Prompt = "Describe the advsntages of this subscription...")]
        [Required(ErrorMessage = "Description required")]
        [StringLength(500, ErrorMessage = "The description is too long")]
        public string Description { get; set; }

        [Display(Name = "Storage limit (GB)", Prompt = "10")]
        [Required(ErrorMessage = "Enter a storage limit")]
        [Range(0, 100, ErrorMessage = "The limit must be in range of 0 - 100")]
        public int? StorageLimitGb { get; set; }

        [Display(Name = "Querries per day limit", Prompt = "1000")]
        [Required(ErrorMessage = "Enter a querries limit")]
        [Range(1, int.MaxValue, ErrorMessage = "The limit must be higher than 0")]
        public int? DailyRequestsLimit { get; set; }

        [Display(Name = "Price ($)", Prompt = "9.99")]
        [Required(ErrorMessage = "Enter a price")]
        [Range(0, 100000, ErrorMessage = "A price can not be negative")]
        public decimal? Price { get; set; }
    }
}
