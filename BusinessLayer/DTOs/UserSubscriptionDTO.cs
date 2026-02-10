using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class UserSubscriptionDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int SubscriptionPlanId { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePaid { get; set; }
        public bool IsAutoRenewEnabled { get; set; }
    }
}
