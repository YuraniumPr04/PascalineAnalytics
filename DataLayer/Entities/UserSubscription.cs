using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int SubscriptionPlanId { get; set; }
        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePaid { get; set; }
        public bool IsAutoRenewEnabled { get; set; } = true;
    }
}
