using BusinessLayer;
using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApp.Areas.Admin.Models.UserSubscription;
namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "admin")]
    public class UserSubscriptionController : Controller
    {
        private ServiceManager _serviceManager;
        public UserSubscriptionController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {

            var subscriptions = await _serviceManager.Subscriptions.GetAllUserSubscriptionsAsync();

            var model = new IndexViewModel
            {
                Subscriptions = subscriptions
            };

            return View(model);
        }
    }
}
