using BusinessLayer.DTOs;
using System.Collections.Generic;

namespace WebApp.Areas.Admin.Models.UserSubscription
{
    public class IndexViewModel
    {
        public IEnumerable<UserSubscriptionDTO> Subscriptions { get; set; }
    }
}