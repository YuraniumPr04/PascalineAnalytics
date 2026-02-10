using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class SubscriptionPlanDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public int StorageLimitGb { get; set; }
        public int DailyRequestsLimit { get; set; }
        public string? Description { get; set; }
    }
}
