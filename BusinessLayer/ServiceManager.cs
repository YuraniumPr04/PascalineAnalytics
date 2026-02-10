using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ServiceManager
    {
        private DataManager _dataManager;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;
        public ServiceManager(DataManager dataManager,
            ISubscriptionService subscriptionService,
            IUserService userService)
        {
            _dataManager = dataManager;
            _subscriptionService = subscriptionService;
            _userService = userService;
        }
        public ISubscriptionService Subscriptions { get { return _subscriptionService; } }
        public IUserService Users { get { return _userService; } }
    }
}   
