using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataManager
    {
        private IBaseRepository<UserSubscription> _userSubscriptionRepository;
        private IBaseRepository<SubscriptionPlan> _subscriptionPlanRepository;

        public DataManager(IBaseRepository<UserSubscription> userSubscriptionRepository,IBaseRepository<SubscriptionPlan> subscriptionPlanRepository)
        { 
            _userSubscriptionRepository = userSubscriptionRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        public IBaseRepository<UserSubscription> UserSubscriptions { get { return _userSubscriptionRepository; } }
        public IBaseRepository<SubscriptionPlan> SubscriptionPlans { get { return _subscriptionPlanRepository; } }
    }
}
